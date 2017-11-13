using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private IEFContext db;

        public ApartmentRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(Apartment apartment)
        {
            db.Set<Apartment>().Add(apartment);
        }

        public void Delete(int id)
        {
            var apartment = GetById(id);

            if (apartment != null)
            {
                db.Set<Apartment>().Remove(apartment);
            }
        }

        public IQueryable<Apartment> GetAll()
        {
            return db.Set<Apartment>();
        }

        public Apartment GetById(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
