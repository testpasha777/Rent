using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Entities;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private IEFContext db;

        public UserProfileRepository(IEFContext _db)
        {
            db = _db;
        }

        public void Create(UserProfile userProfile)
        {
            db.Set<UserProfile>().Add(userProfile);
        }

        public void Delete(string id)
        {
            var userProfile = Get(id);

            if(userProfile != null)
            {
                db.Set<UserProfile>().Remove(userProfile);
            }
        }

        public UserProfile Get(string id)
        {
            return db.Set<UserProfile>().Find(id);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
