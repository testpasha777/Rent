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
        bool Create(CountryCreateViewModel countryVM);
        void Delete(int id);
        bool Update(CountryEditViewModel countryVM);
        CountryViewModel GetById(int id);
        CountryViewModel GetByName(string name);
        CountryEditViewModel GetCountryEditById(int id);
        IEnumerable<CountryViewModel> GetAll();
    }
}
