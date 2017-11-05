using Autofac;
using Autofac.Integration.Mvc;
using BLL.Infrastructure.Identity;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Rent.App_Start.Startup))]

namespace Rent.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DataModule("RentConnection", app));

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(MyOptions.OptionCookies());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //var facebookOptions = new FacebookAuthenticationOptions
            //{
            //    AppId = "350879062040009",
            //    AppSecret = "f1105a9743238e20671f63d37b373298"
            //};

            //facebookOptions.Scope.Add("email");

            //facebookOptions.Fields.Add("name");
            //facebookOptions.Fields.Add("email");

            //app.UseFacebookAuthentication(facebookOptions);

            //var googleOptions = new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "267725092279-4978t08p88snbdh96jlsutb7jgop564g.apps.googleusercontent.com",
            //    ClientSecret = "QqPMt4Z7ARZrXCJdFVod0g-e",
            //};

            //googleOptions.Scope.Add("profile");
            //googleOptions.Scope.Add("email");
            //googleOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");

            //app.UseGoogleAuthentication(googleOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "267725092279-4978t08p88snbdh96jlsutb7jgop564g.apps.googleusercontent.com",
            //    ClientSecret = "QqPMt4Z7ARZrXCJdFVod0g-e"
            //});

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}