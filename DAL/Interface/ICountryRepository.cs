using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICountryRepository
    {
        Country GetById(int id);
        Country GetByName(string name);
        IQueryable<Country> GetAll();
        void Create(Country country);
        void Delete(int id);
        void SaveChanges();
    }
}
