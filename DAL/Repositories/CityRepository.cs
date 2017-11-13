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
            var city = GetById(id);

            if(city != null)
            {
                db.Set<City>().Remove(city);
            }
        }

        public City GetById(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public City GetCityInCountry(int countryId, string cityName)
        {
            return GetAll().SingleOrDefault(i => i.CountryId == countryId && i.Name == cityName);
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
