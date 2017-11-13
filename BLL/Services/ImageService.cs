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

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private DropboxClient dropBoxClient;
        private string imgPath;

        public ImageService(DropboxClient _dropBoxClient)
        {
            dropBoxClient = _dropBoxClient;
            imgPath = "";
        }

        public Bitmap CreateImage(HttpPostedFileBase image, int maxWidth, int maxHeight)
        {
            if (image != null && image.ContentLength != 0)
            {
                try
                {
                    using (Bitmap originalPic = new Bitmap(image.InputStream, true))
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

        public async Task<string> Upload(Bitmap image, string folder, string fileName)
        {
            using (var memory = new MemoryStream(ImageToByte(image)))
            {
               var upload = await dropBoxClient.Files.UploadAsync("/" + folder + "/" + fileName, body: memory);
               imgPath = upload.PathLower;
               return upload.PathLower;
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
