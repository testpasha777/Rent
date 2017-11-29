using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IApartmentService
    {
        ApartmentCreateViewModel GetCreateApartment();
        bool Create(ApartmentCreateViewModel createVM, string userId, string userName, string folderName);
        ApartmentViewModel Get(int id);
    }
}
