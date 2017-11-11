using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class AvailableToGuestRepository : IAvailableToGuestRepository
    {
        private IEFContext db;

        public AvailableToGuestRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(AvailableToGuest availableToGuest)
        {
            db.Set<AvailableToGuest>().Add(availableToGuest);
        }

        public void Delete(int id)
        {
            var availableToGuest = GetAll().SingleOrDefault(i => i.Id == id);

            if(availableToGuest != null)
            {
                db.Set<AvailableToGuest>().Remove(availableToGuest);
            }
        }

        public AvailableToGuest GetById(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public AvailableToGuest GetByName(string name)
        {
            return GetAll().SingleOrDefault(i => i.Name == name);
        }

        public IQueryable<AvailableToGuest> GetAll()
        {
            return db.Set<AvailableToGuest>();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
