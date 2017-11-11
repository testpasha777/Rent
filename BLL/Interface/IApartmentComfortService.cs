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
        bool Create(ApartmentComfortCreateViewModel apartmentComfortVM);
        void Delete(int id);
        bool Update(ApartmentComfortEditViewModel apartmentComfortEditVM);
        ApartmentComfortViewModel GetById(int id);
        ApartmentComfortEditViewModel GetEditById(int id);
        IEnumerable<ApartmentComfortViewModel> GetAll();
    }
}
