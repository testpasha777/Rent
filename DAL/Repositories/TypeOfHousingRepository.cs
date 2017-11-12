using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class TypeOfHousingRepository : ITypeOfHousingRepository
    {
        private IEFContext db;

        public TypeOfHousingRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(TypeOfHousing typeOfHousing)
        {
            db.Set<TypeOfHousing>().Add(typeOfHousing);
        }

        public void Delete(int id)
        {
            var typeOfHousing = GetById(id);

            if (typeOfHousing != null)
            {
                db.Set<TypeOfHousing>().Remove(typeOfHousing);
            }
        }

        public IQueryable<TypeOfHousing> GetAll()
        {
            return db.Set<TypeOfHousing>();
        }

        public TypeOfHousing GetById(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public TypeOfHousing GetByName(string name)
        {
            return GetAll().SingleOrDefault(i => i.Name == name);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
