using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using BLL.Infrastructure.Identity;
using System.Web;
using System.Net;
using Microsoft.Owin.Security;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class AccountIdentityService : IAccountService
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;
        private IAuthenticationManager authenticationManager;

        public AccountIdentityService(ApplicationUserManager _userManager, ApplicationSignInManager _signInManager, IAuthenticationManager _authManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            authenticationManager = _authManager;
        }

        public StatusAccountViewModel CreateLogin(string email)
        {
            var info = authenticationManager.GetExternalLoginInfo();
            var user = new AppUser
            {
                UserName = email,
                Email = email
            };

            var result = userManager.Create(user);

            if(result.Succeeded)
            {
                result = userManager.AddLogin(user.Id, info.Login);

                if(result.Succeeded)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return StatusAccountViewModel.Success;
                }
            }

            return StatusAccountViewModel.Error;
        }

        public SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            return signInManager.ExternalSignIn(loginInfo, isPersistent: false);
        }

        public ExternalLoginInfo GetExternalLoginInfo()
        {
            return authenticationManager.GetExternalLoginInfo();
        }

        public StatusAccountViewModel Login(LoginViewModel login)
        {
            var result = signInManager.PasswordSignIn(login.Email, login.Password,
                login.IsRememberme, shouldLockout: false);

            if(result == SignInStatus.Success)
            {
                return StatusAccountViewModel.Success;
            }

            return StatusAccountViewModel.Error;
        }

        public void Logout()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public StatusAccountViewModel Register(RegisterViewModel register)
        {
            var user = new AppUser
            {
                UserName = register.Email,
                Email = register.Email
            };

            var result = userManager.Create(user, register.Password);

            if(result.Succeeded)
            {
                return StatusAccountViewModel.Success;
            }

            return StatusAccountViewModel.Error;
        }

        public bool SendTwoFactorCode(string provider)
        {
            return signInManager.SendTwoFactorCode(provider);
        }

        public IList<string> UserFactors()
        {
            var userId = signInManager.GetVerifiedUserId();

            if(userId == null)
            {
                return null;
            }

            var userFactors = userManager.GetValidTwoFactorProviders(userId);
            return userFactors;
        }

        public IEnumerable<string> UserRoles(string email)
        {
            throw new NotImplementedException();
        }
    }
}
