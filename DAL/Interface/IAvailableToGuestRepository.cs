using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IAvailableToGuestRepository
    {
        AvailableToGuest Get(int id);
        IQueryable<AvailableToGuest> GetAll();
        void Create(AvailableToGuest availableToGuest);
        void Delete(int id);
        void SaveChanges();
    }
}
