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
        bool AddApartmentComfort(ApartmentComfortCreateViewModel apartmentComfortVM);
        void DeleteApartmentComfort(int id);
        bool UpdateApartmentComfort(ApartmentComfortEditViewModel apartmentComfortEditVM);
        ApartmentComfortViewModel GetApartmentComfort(int id);
        ApartmentComfortEditViewModel GetEditApartmentComfort(int id);
        IEnumerable<ApartmentComfortViewModel> GetAllApartmentComfort();
    }
}
