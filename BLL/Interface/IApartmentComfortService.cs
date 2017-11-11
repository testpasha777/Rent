using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IApartmentComfortService
    {
        bool AddApartmentComfort(ApartmentComfortViewModel apartmentComfort);
        void DeleteApartmentComfort(int id);
        bool UpdateApartmentComfort(ApartmentComfortEditViewModel apartmentComfortEdit);
        ApartmentComfortViewModel GetApartmentComfort(int id);
        IEnumerable<ApartmentComfortViewModel> GetApartmentComforts();
    }
}
