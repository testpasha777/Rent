using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private IEFContext db;

        public CountryRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(Country country)
        {
            db.Set<Country>().Add(country);
        }

        public void Delete(int id)
        {
            var country = GetAll().SingleOrDefault(i => i.Id == id);

            if(country != null)
            {
                db.Set<Country>().Remove(country);
            }
        }

        public Country Get(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public IQueryable<Country> GetAll()
        {
            return db.Set<Country>();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
