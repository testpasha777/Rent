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

        public ApartmentService(IApartmentRepository _apartmentRep,
            IAvailableToGuestService _availableToGuestService,
            ITypeOfHousingService _typeOfHousingService)
        {
            apartmentRep = _apartmentRep;
            availableToGuestService = _availableToGuestService;
            typeOfHousingService = _typeOfHousingService;
        }

        public ApartmentCreateViewModel GetCreateApartment()
        {
            ApartmentCreateViewModel apartmentCreate = new ApartmentCreateViewModel();
            apartmentCreate.AvailableToGuest = availableToGuestService.GetAll().ToList();
            apartmentCreate.TypeOfHousing = typeOfHousingService.GetAll().ToList();

            return apartmentCreate;
        }
    }
}
