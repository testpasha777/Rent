using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;
using System.IO;
using Dropbox.Api;
using System.ComponentModel;
using DAL.Interface;
using DAL.Entities.Entities;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private DropboxClient dropBoxClient;
        private IApartmentImageRepository apartmentImageRep;

        public ImageService(DropboxClient _dropBoxClient,
            IApartmentImageRepository _apartmentImageRep)
        {
            dropBoxClient = _dropBoxClient;
            apartmentImageRep = _apartmentImageRep;
        }

        public Bitmap CreateImage(Bitmap image, int maxWidth, int maxHeight)
        {
            if (image != null)
            {
                try
                {
                    using (Bitmap originalPic = image)
                    {
                        int originalWidth = originalPic.Width;
                        int originalHeight = originalPic.Height;
                        // To preserve the aspect ratio
                        float ratioX = (float)maxWidth / (float)originalWidth;
                        float ratioY = (float)maxHeight / (float)originalHeight;
                        float ratio = Math.Min(ratioX, ratioY);
                        // New width and height based on aspect ratio
                        int width = (int)(originalWidth * ratio);
                        int height = (int)(originalHeight * ratio);

                        using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                        {
                            using (Graphics oGraphics = Graphics.FromImage(outBmp))
                            {
                                oGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                oGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                oGraphics.DrawImage(originalPic, 0, 0, width, height);
                                return new Bitmap(outBmp);
                            }
                        }
                    }
                }
                catch { }
            }

            return null;
        }

        public async void CheckImages()
        {
            var images = apartmentImageRep.GetAll().Where(i => i.Local == true).ToList();

            if(images.Count == 0)
            {
                return;
            }

            for(int i = 0; i < images.Count(); i++)
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                string localDeletepath = images[i].PathPhoto;
                Bitmap img = new Bitmap(dir + images[i].PathPhoto);
                images[i].ApartmentId = images[i].ApartmentId;
                images[i].PathPhoto = await Upload(img, images[i].FolderName, images[i].FileName);
                images[i].LinkPhoto = await SharedFile(images[i].PathPhoto);
                img.Dispose();
                DeleteLocal(dir + localDeletepath);
                images[i].FileName = images[i].FileName;
                images[i].FolderName = images[i].FolderName;
                images[i].Local = false;
            }

            apartmentImageRep.SaveChanges();
        }

        public async Task<string> Upload(Bitmap image, string folder, string fileName)
        {
            using (var memory = new MemoryStream(ImageToByte(image)))
            {
               var upload = await dropBoxClient.Files.UploadAsync("/" + folder + "/" + fileName, body: memory);
               return upload.PathLower;
            }
        }

        public string SaveLocal(Bitmap image, string folder, string fileName)
        {
            string path = Path.Combine(folder, fileName);
            image.Save(path, ImageFormat.Jpeg);
            return path;
        }

        public void DeleteLocal(string path)
        {
            FileInfo file = new FileInfo(path);

            if(file.Exists)
            {
                file.Delete();
            }
        }

        public async Task<string> SharedFile(string path)
        {
            var shared = await dropBoxClient.Sharing.CreateSharedLinkWithSettingsAsync(path);
            return ConvertUrlToDlUrl(shared.Url);
        }

        public async Task DeleteFile(string path)
        {
            await dropBoxClient.Files.DeleteV2Async(path);
        }

        public Bitmap Base64ToBitmap(string base64String)
        {
            byte[] img = Convert.FromBase64String(base64String);


            using (var ms = new MemoryStream(img, 0, img.Length))
            {
                Bitmap image = new Bitmap(ms);
                return image;
            }
        }

        public Bitmap HttpPostedFileBaseToBitmap(HttpPostedFileBase image)
        {
            return new Bitmap(image.InputStream, true);
        }

        private string ConvertUrlToDlUrl(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com").Replace("?dl=0", "");
        }

        private byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
