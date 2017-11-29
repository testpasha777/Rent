using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Interface
{
    public interface IImageService
    {
        Task<string> Upload(Bitmap image, string folder, string fileName);
        string SaveLocal(Bitmap image, string folder, string fileName);
        Task<string> SharedFile(string path);
        Task DeleteFile(string path);
        Bitmap HttpPostedFileBaseToBitmap(HttpPostedFileBase image);
        Bitmap Base64ToBitmap(string base64String);
        Bitmap CreateImage(Bitmap image, int maxWidth, int maxHeight);
        void CheckImages();
    }
}
