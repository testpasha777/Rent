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
    public class CityService : ICityService
    {
        private ICityRepository cityRep;
        private ICountryRepository countryRep;

        public CityService(ICityRepository _cityRep, ICountryRepository _countryRep)
        {
            cityRep = _cityRep;
            countryRep = _countryRep;
        }

        public bool Add(CityCreateViewModel cityVM)
        {
            var city = cityRep.GetCityInCountry(cityVM.CountryId, cityVM.Name);

            if(city != null)
            {
                return false;
            }

            City add = new City();
            add.Name = cityVM.Name;
            add.CountryId = cityVM.CountryId;
            cityRep.Create(add);
            cityRep.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            cityRep.Delete(id);
            cityRep.SaveChanges();
        }

        public IEnumerable<CityViewModel> GetAll()
        {
            var cities = cityRep.GetAll().Select(i => new CityViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Country = i.Country.Name
            });

            return cities;
        }

        public CityViewModel GetById(int id)
        {
            var city = cityRep.GetById(id);

            return new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country.Name
            };
        }

        public CityCreateViewModel GetCreateCity()
        {
            CityCreateViewModel cityCreate = new CityCreateViewModel();
            cityCreate.Countries = countryRep.GetAll().Select(i => new SelectItemViewModel
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            return cityCreate;
        }

        public CityEditViewModel GetEditCityById(int id)
        {
            var city = cityRep.GetById(id);
            CityEditViewModel cityVM = new CityEditViewModel();
            cityVM.Id = city.Id;
            cityVM.Name = city.Name;
            cityVM.CountryId = city.CountryId;
            cityVM.Countries = countryRep.GetAll().Select(i => new SelectItemViewModel
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            return cityVM;
        }

        public bool Update(CityEditViewModel cityVM)
        {
            var searchCity = cityRep.GetCityInCountry(cityVM.CountryId, cityVM.Name);

            if(searchCity != null)
            {
                return false;
            }

            var oldCity = cityRep.GetById(cityVM.Id);

            oldCity.Name = cityVM.Name;
            oldCity.CountryId = cityVM.CountryId;
            cityRep.SaveChanges();
            return true;
        }
    }
}
