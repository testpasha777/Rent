using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICityRepository
    {
        City GetById(int id);
        City GetCityInCountry(int countryId, string cityName);
        IQueryable<City> GetAll();
        void Create(City city);
        void Delete(int id);
        void SaveChanges();
    }
}
