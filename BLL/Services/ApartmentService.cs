using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Interface;
using DAL.Entities.Entities;

namespace BLL.Services
{
    public class ApartmentService : IApartmentService
    {
        private IApartmentRepository apartmentRep;
        private IAvailableToGuestService availableToGuestService;
        private ITypeOfHousingService typeOfHousingService;
        private IApartmentComfortService apartmentComfortService;
        private IImageService imageService;
        private ICountryService countryService;
        private ICityService cityService;

        public ApartmentService(IApartmentRepository _apartmentRep,
            IAvailableToGuestService _availableToGuestService,
            ITypeOfHousingService _typeOfHousingService,
            IApartmentComfortService _apartmentComfortService,
            IImageService _imageService,
            ICountryService _countryService,
            ICityService _cityService)
        {
            apartmentRep = _apartmentRep;
            availableToGuestService = _availableToGuestService;
            typeOfHousingService = _typeOfHousingService;
            apartmentComfortService = _apartmentComfortService;
            imageService = _imageService;
            countryService = _countryService;
            cityService = _cityService;
        }

        public bool Create(ApartmentCreateViewModel createVM)
        {
            Apartment create = new Apartment();
            create.AvailableToGuestId = createVM.AvailableToGuestId;
            create.Description = createVM.Description;
            create.Price = createVM.Price;
            create.TypeOfHousingId = createVM.TypeOfHousingId;

            string cityName = createVM.CityAndCountry.Substring(0, createVM.CityAndCountry.IndexOf(','));
            string countryName = createVM.CityAndCountry.Substring(createVM.CityAndCountry.LastIndexOf(' '));

            var country = countryService.GetByName(countryName);

            if(country == null)
            {
                CountryCreateViewModel createCountryVM = new CountryCreateViewModel();
                createCountryVM.Name = countryName;
                countryService.Create(createCountryVM);
            }

            



            return true;
        }

        public ApartmentCreateViewModel GetCreateApartment()
        {
            ApartmentCreateViewModel apartmentCreate = new ApartmentCreateViewModel();
            apartmentCreate.AvailableToGuest = availableToGuestService.GetAll().ToList();
            apartmentCreate.TypeOfHousing = typeOfHousingService.GetAll().ToList();
            apartmentCreate.ApartmentComforts = apartmentComfortService.GetAll().ToList();



            return apartmentCreate;
        }
    }
}
