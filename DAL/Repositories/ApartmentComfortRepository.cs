using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class ApartmentComfortRepository : IApartmentComfortRepository
    {
        private IEFContext db;

        public ApartmentComfortRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(ApartmentComfort comfort)
        {
            db.Set<ApartmentComfort>().Add(comfort);
        }

        public void Delete(int id)
        {
            var apartmentComfort = GetAll().SingleOrDefault(i => i.Id == id);

            if(apartmentComfort != null)
            {
                db.Set<ApartmentComfort>().Remove(apartmentComfort);
            }
        }

        public ApartmentComfort Get(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public IQueryable<ApartmentComfort> GetAll()
        {
            return db.Set<ApartmentComfort>();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
