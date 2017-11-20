using BLL.Interface;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Entities.Entities;

namespace BLL.Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepository countryRep;

        public CountryService(ICountryRepository _countryRep)
        {
            countryRep = _countryRep;
        }

        public bool Create(CountryCreateViewModel countryVM)
        {
            var country = countryRep.GetByName(countryVM.Name);

            if(country != null)
            {
                return false;
            }

            Country add = new Country();
            add.Name = countryVM.Name;
            countryRep.Create(add);
            countryRep.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            countryRep.Delete(id);
            countryRep.SaveChanges();
        }

        public IEnumerable<CountryViewModel> GetAll()
        {
            return countryRep.GetAll().Select(i => new CountryViewModel
            {
                Id = i.Id,
                Name = i.Name
            });
        }

        public CountryViewModel GetById(int id)
        {
            var country = countryRep.GetById(id);

            return new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public CountryViewModel GetByName(string name)
        {
            var country = countryRep.GetByName(name);

            if(country == null)
            {
                return null;
            }

            return new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public CountryEditViewModel GetCountryEditById(int id)
        {
            var country = countryRep.GetById(id);

            return new CountryEditViewModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public bool Update(CountryEditViewModel countryVM)
        {
            var country = countryRep.GetByName(countryVM.Name);

            if(country != null && country.Id != countryVM.Id)
            {
                return false;
            }

            var update = countryRep.GetById(countryVM.Id);
            update.Name = countryVM.Name;
            countryRep.SaveChanges();
            return true;
        }
    }
}
