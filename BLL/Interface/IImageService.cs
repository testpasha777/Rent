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
        Bitmap CreateImage(HttpPostedFileBase image, int maxWidth, int maxHeight);
        Task<string> Upload(Bitmap image, string folder, string fileName);
        Task<string> SharedFile(string path);
        Task DeleteFile(string path);
    }
}
