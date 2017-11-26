using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Interface;
using DAL.Entities.Entities;
using System.Threading;
using System.Web.Hosting;

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
        private IApartmentComfortRepository apartmentComfortRep;
        private IApartmentImageRepository apartmentImageRep;

        public ApartmentService(IApartmentRepository _apartmentRep,
            IAvailableToGuestService _availableToGuestService,
            ITypeOfHousingService _typeOfHousingService,
            IApartmentComfortService _apartmentComfortService,
            IImageService _imageService,
            ICountryService _countryService,
            ICityService _cityService,
            IApartmentComfortRepository _apartmentComfortRep,
            IApartmentImageRepository _apartmentImageRep)
        {
            apartmentRep = _apartmentRep;
            availableToGuestService = _availableToGuestService;
            typeOfHousingService = _typeOfHousingService;
            apartmentComfortService = _apartmentComfortService;
            imageService = _imageService;
            countryService = _countryService;
            cityService = _cityService;
            apartmentComfortRep = _apartmentComfortRep;
            apartmentImageRep = _apartmentImageRep;
        }

        public bool Create(ApartmentCreateViewModel createVM, string userId, string userName)
        {
            Apartment create = new Apartment();
            create.AvailableToGuestId = createVM.AvailableToGuestId;
            create.Description = createVM.Description;
            create.Price = createVM.Price;
            create.TypeOfHousingId = createVM.TypeOfHousingId;
            create.Address = createVM.Street + " " + createVM.StreetNumber;
            create.MaxNumberOfGuests = createVM.MaxNumberOfGuests;
            create.UserProfileId = userId;

            if(createVM.FletNumber != null)
            {
                create.Address += "/" + createVM.FletNumber;
            }

            FillApartmentCountryAndCity(create, createVM.CityAndCountry);
            
            if (createVM.SelectedApartmentComfortsId != null)
            {
                for (int i = 0; i < createVM.SelectedApartmentComfortsId.Count; i++)
                {
                    create.ApartmentComforts.Add(apartmentComfortRep.GetById(Int32.Parse(createVM.SelectedApartmentComfortsId[i])));
                }
            }

            apartmentRep.Create(create);
            apartmentRep.SaveChanges();

            Task.Factory.StartNew(() => FillApartmentImages(createVM.images.Length, createVM.images, create.Id, userName), TaskCreationOptions.LongRunning);
            Thread.Sleep(50000);
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

        private async void FillApartmentImages(int countImg, string[] images, int apartmentId, string folderName)
        {
            for (int i = 0; i < countImg; i++)
            {
                var bitMapImg = imageService.Base64ToBitmap(images[i].Substring(23));
                var newImg = imageService.CreateImage(bitMapImg, 1600, 600);

                string guid = Guid.NewGuid().ToString();
                ApartmentImage createApartmentImage = new ApartmentImage();
                createApartmentImage.ApartmentId = apartmentId;
                createApartmentImage.PathPhoto = await imageService.Upload(newImg, folderName, guid + ".jpg");
                createApartmentImage.LinkPhoto = await imageService.SharedFile(createApartmentImage.PathPhoto);
                apartmentImageRep.Create(createApartmentImage);
            }

            apartmentImageRep.SaveChanges();
        }

        private void FillApartmentCountryAndCity(Apartment create, string cityAndCountryName)
        {
            string cityName = cityAndCountryName.Substring(0, cityAndCountryName.IndexOf(','));
            string countryName = cityAndCountryName.Substring(cityAndCountryName.LastIndexOf(' '));

            var country = countryService.GetByName(countryName);

            if (country == null)
            {
                CountryCreateViewModel createCountryVM = new CountryCreateViewModel();
                createCountryVM.Name = countryName;
                country = countryService.Create(createCountryVM);

                var city = cityService.GetCityInCountry(country.Id, cityName);

                if (city == null)
                {
                    CityCreateViewModel createCityVM = new CityCreateViewModel()
                    {
                        Name = cityName,
                        CountryId = country.Id
                    };

                    city = cityService.Create(createCityVM);
                    create.CityId = city.Id;
                }
            }
            else
            {
                var city = cityService.GetCityInCountry(country.Id, cityName);

                if (city == null)
                {
                    CityCreateViewModel createCityVM = new CityCreateViewModel()
                    {
                        Name = cityName,
                        CountryId = country.Id
                    };

                    city = cityService.Create(createCityVM);
                    create.CityId = city.Id;
                }

                create.CityId = city.Id;
            }
        }
    }
}
