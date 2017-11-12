using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ITypeOfHousingRepository
    {
        TypeOfHousing GetById(int id);
        TypeOfHousing GetByName(string name);
        IQueryable<TypeOfHousing> GetAll();
        void Create(TypeOfHousing typeOfHousing);
        void Delete(int id);
        void SaveChanges();
    }
}
