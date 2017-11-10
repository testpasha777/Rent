using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private IEFContext db;

        public CityRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(City city)
        {
            db.Set<City>().Add(city);
        }

        public void Delete(int id)
        {
            var city = GetAll().SingleOrDefault(i => i.Id == id);

            if(city != null)
            {
                db.Set<City>().Remove(city);
            }
        }

        public City Get(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public IQueryable<City> GetAll()
        {
            return db.Set<City>();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
