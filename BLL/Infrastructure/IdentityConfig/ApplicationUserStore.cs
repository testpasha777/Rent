using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.IdentityConfig
{
    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(EFContext context) : base(context)
        {

        }
    }
}
