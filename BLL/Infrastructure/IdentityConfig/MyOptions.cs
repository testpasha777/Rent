using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity
{
    public class MyOptions
    {
        public static CookieAuthenticationOptions OptionCookies()
        {
            return new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, AppUser>(
                        validateInterval: TimeSpan.FromMinutes(365),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            };
        }
    }
}
