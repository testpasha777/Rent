using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICountryService
    {
        bool Add(CountryCreateViewModel countryVM);
        void Delete(int id);
        bool Update(CountryEditViewModel countryVM);
        CountryViewModel GetById(int id);
        CountryEditViewModel GetCountryEditById(int id);
        IEnumerable<CountryViewModel> GetAll();
    }
}
