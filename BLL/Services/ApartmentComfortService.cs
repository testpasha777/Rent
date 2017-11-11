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
    public class ApartmentComfortService : IApartmentComfortService
    {
        private IApartmentComfortRepository apartmentComfortRep;

        public ApartmentComfortService(IApartmentComfortRepository _apartmentComfortRep)
        {
            apartmentComfortRep = _apartmentComfortRep;
        }

        public bool Create(ApartmentComfortCreateViewModel apartmentComfortVM)
        {
            var apartmentComfort = apartmentComfortRep.GetByName(apartmentComfortVM.Name);

            if(apartmentComfort != null)
            {
                return false;
            }

            ApartmentComfort add = new ApartmentComfort();
            add.Name = apartmentComfortVM.Name;
            apartmentComfortRep.Create(add);
            apartmentComfortRep.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            apartmentComfortRep.Delete(id);
            apartmentComfortRep.SaveChanges();
        }

        public ApartmentComfortViewModel GetById(int id)
        {
            var apartmentComfort = apartmentComfortRep.GetById(id);

            return new ApartmentComfortViewModel
            {
                Id = apartmentComfort.Id,
                Name = apartmentComfort.Name
            };
        }

        public ApartmentComfortEditViewModel GetEditById(int id)
        {
            var apartmentComfort = apartmentComfortRep.GetById(id);

            return new ApartmentComfortEditViewModel
            {
                Id = apartmentComfort.Id,
                Name = apartmentComfort.Name
            };
        }

        public IEnumerable<ApartmentComfortViewModel> GetAll()
        {
            var apartmentComforts = apartmentComfortRep.GetAll().Select(i => new ApartmentComfortViewModel
            {
                Id = i.Id,
                Name = i.Name
            });

            return apartmentComforts;
        }

        public bool Update(ApartmentComfortEditViewModel apartmentComfortEditVM)
        {
            var apartmentComfort = apartmentComfortRep.GetById(apartmentComfortEditVM.Id);

            if(apartmentComfort == null)
            {
                return false;
            }

            apartmentComfort.Name = apartmentComfortEditVM.Name;
            apartmentComfortRep.SaveChanges();
            return true;
        }
    }
}
