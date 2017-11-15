using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Interface;

namespace BLL.Services
{
    public class ApartmentService : IApartmentService
    {
        private IApartmentRepository apartmentRep;
        private IAvailableToGuestService availableToGuestService;
        private ITypeOfHousingService typeOfHousingService;
        private IApartmentComfortService apartmentComfortService;

        public ApartmentService(IApartmentRepository _apartmentRep,
            IAvailableToGuestService _availableToGuestService,
            ITypeOfHousingService _typeOfHousingService,
            IApartmentComfortService _apartmentComfortService)
        {
            apartmentRep = _apartmentRep;
            availableToGuestService = _availableToGuestService;
            typeOfHousingService = _typeOfHousingService;
            apartmentComfortService = _apartmentComfortService;
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
