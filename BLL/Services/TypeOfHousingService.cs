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
    public class TypeOfHousingService : ITypeOfHousingService
    {
        private ITypeOfHousingRepository typeOfHousingRep;

        public TypeOfHousingService(ITypeOfHousingRepository _typeOfHousingRep)
        {
            typeOfHousingRep = _typeOfHousingRep;
        }

        public bool Create(TypeOfHousingCreateViewModel typeOfHousingVM)
        {
            var typeOfHousing = typeOfHousingRep.GetByName(typeOfHousingVM.Name);

            if(typeOfHousing != null)
            {
                return false;
            }

            TypeOfHousing create = new TypeOfHousing();
            create.Name = typeOfHousingVM.Name;
            typeOfHousingRep.Create(create);
            typeOfHousingRep.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            typeOfHousingRep.Delete(id);
            typeOfHousingRep.SaveChanges();
        }

        public IEnumerable<TypeOfHousingViewModel> GetAll()
        {
            var typesOfHousing = typeOfHousingRep.GetAll().Select(i => new TypeOfHousingViewModel
            {
                Id = i.Id,
                Name = i.Name
            });

            return typesOfHousing;
        }

        public TypeOfHousingViewModel GetById(int id)
        {
            var typeOfHousing = typeOfHousingRep.GetById(id);

            return new TypeOfHousingViewModel
            {
                Id = typeOfHousing.Id,
                Name = typeOfHousing.Name
            };
        }

        public TypeOfHousingEditViewModel GetEditById(int id)
        {
            var typeOfHousing = typeOfHousingRep.GetById(id);

            return new TypeOfHousingEditViewModel
            {
                Id = typeOfHousing.Id,
                Name = typeOfHousing.Name
            };
        }

        public bool Update(TypeOfHousingEditViewModel typeOfHousingVM)
        {
            var typeOfHousing = typeOfHousingRep.GetByName(typeOfHousingVM.Name);

            if(typeOfHousing != null && typeOfHousing.Id != typeOfHousingVM.Id)
            {
                return false;
            }

            var update = typeOfHousingRep.GetById(typeOfHousingVM.Id);
            update.Name = typeOfHousingVM.Name;
            typeOfHousingRep.SaveChanges();
            return true;
        }
    }
}
