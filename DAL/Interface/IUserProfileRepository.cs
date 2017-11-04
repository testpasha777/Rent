using DAL.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserProfileRepository
    {
        UserProfile Get(string id);
        void Create(UserProfile userProfile);
        void Delete(string id);
        void SaveChange();
    }
}
