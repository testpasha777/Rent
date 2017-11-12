using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICityService
    {
        bool Create(CityCreateViewModel cityVM);
        void Delete(int id);
        bool Update(CityEditViewModel cityVM);
        CityViewModel GetById(int id);
        CityCreateViewModel GetCreateCity();
        CityEditViewModel GetEditCityById(int id);
        IEnumerable<CityViewModel> GetAll();
    }
}
