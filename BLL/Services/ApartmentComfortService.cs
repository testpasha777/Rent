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
    public class ApartmentComfortService : IApartmentComfortService
    {
        private IApartmentComfortRepository apartmentComfortRep;

        public ApartmentComfortService(IApartmentComfortRepository _apartmentComfortRep)
        {
            apartmentComfortRep = _apartmentComfortRep;
        }

        public bool AddApartmentComfort(ApartmentComfortViewModel apartmentComfort)
        {
            throw new NotImplementedException();
        }

        public void DeleteApartmentComfort(int id)
        {
            throw new NotImplementedException();
        }

        public ApartmentComfortViewModel GetApartmentComfort(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApartmentComfortViewModel> GetApartmentComforts()
        {
            throw new NotImplementedException();
        }

        public bool UpdateApartmentComfort(ApartmentComfortEditViewModel apartmentComfortEdit)
        {
            throw new NotImplementedException();
        }
    }
}
