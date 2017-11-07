using Autofac;
using Autofac.Core;
using BLL.Infrastructure.Identity;
using BLL.Infrastructure.IdentityConfig;
using BLL.Interface;
using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Interface;
using DAL.Repositories;
using Dropbox.Api;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            builder.RegisterType<ApplicationRoleStore>().As<IRoleStore<IdentityRole, string>>().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();
            builder.Register(c => new DropboxClient("5nsmQQ0lxRAAAAAAAAAADpJb5tXhe7g_UP4qjke8DyZfMDYB8UUCJNfjC31WQBEC")).AsSelf().InstancePerRequest();

            //Repository
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>().InstancePerRequest();

            //Services
            builder.RegisterType<AccountIdentityService>().As<IAccountService>().InstancePerRequest();
            builder.RegisterType<ImageService>().As<IImageService>().InstancePerRequest();
            

            base.Load(builder);
        }
    }
}
