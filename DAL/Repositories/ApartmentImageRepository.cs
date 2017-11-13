using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;

namespace DAL.Repositories
{
    public class ApartmentImageRepository : IApartmentImageRepository
    {
        private IEFContext db;

        public ApartmentImageRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(ApartmentImage apartmentImage)
        {
            db.Set<ApartmentImage>().Add(apartmentImage);
        }

        public void Delete(int id)
        {
            var apartmentImage = GetById(id);

            if(apartmentImage != null)
            {
                db.Set<ApartmentImage>().Remove(apartmentImage);
            }
        }

        public IQueryable<ApartmentImage> GetAll()
        {
            return db.Set<ApartmentImage>();
        }

        public ApartmentImage GetById(int id)
        {
            return GetAll().SingleOrDefault(i => i.Id == id);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
