using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IApartmentComfortRepository
    {
        ApartmentComfort GetById(int id);
        ApartmentComfort GetByName(string name);
        IQueryable<ApartmentComfort> GetAll();
        void Create(ApartmentComfort comfort);
        void Delete(int id);
        void SaveChanges();
    }
}
