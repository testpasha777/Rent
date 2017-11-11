using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IApartmentRepository
    {
        void Create(Apartment apartment);
        void Delete(int id);
        Apartment GetById(int id);
        IQueryable<Apartment> GetAll();
        void SaveChanges();
    }
}
