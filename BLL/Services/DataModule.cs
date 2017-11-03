using Autofac;
using BLL.Infrastructure.Identity;
using BLL.Infrastructure.IdentityConfig;
using BLL.Interface;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class DataModule : Module
    {
        private string connStr;
        private IAppBuilder app;

        public DataModule(string _connStr, IAppBuilder _app)
        {
            connStr = _connStr;
            app = _app;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFContext(connStr)).AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<AccountIdentityService>().As<IAccountService>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
