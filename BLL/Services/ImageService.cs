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

namespace BLL.Services
{
    public class ImageService : IImageService
    {

        public ImageService()
        {
            
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
    }
}
