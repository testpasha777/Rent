using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IApartmentImageRepository
    {
        void Create(ApartmentImage apartmentImage);
        void Delete(int id);
        ApartmentImage GetById(int id);
        IQueryable<ApartmentImage> GetAll();
        IQueryable<ApartmentImage> GetImagesByApartmentId(int id);
        void SaveChanges();
    }
}
