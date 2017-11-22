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
using DAL.Interface;
using DAL.Repositories;
using DAL.Entities.Entities;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;
        private IAuthenticationManager authenticationManager;
        private IUserProfileRepository userProfileRepository;
        private IImageService imgService;
        private IUnitOfWork unitOfWork;

        public AccountService(ApplicationUserManager _userManager,
            ApplicationSignInManager _signInManager,
            IAuthenticationManager _authManager,
            IUserProfileRepository _userProfile,
            IImageService _imgService,
            IUnitOfWork _unitOfWork)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            authenticationManager = _authManager;
            userProfileRepository = _userProfile;
            imgService = _imgService;
            unitOfWork = _unitOfWork;
        }

        public StatusAccountViewModel CreateLogin(string email, string name, string surName)
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
                    UserProfile userProfile = new UserProfile();
                    userProfile.Name = name;
                    userProfile.SurName = surName;
                    userProfile.Id = user.Id;
                    userProfileRepository.Create(userProfile);
                    userProfileRepository.SaveChanges();
                    userManager.AddToRole(user.Id, "User");
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

        public async Task<StatusAccountViewModel> Register(RegisterViewModel register)
        {
            try
            {
                unitOfWork.StartTransaction();
                var user = new AppUser
                {
                    UserName = register.Email,
                    Email = register.Email
                };

                var result = userManager.Create(user, register.Password);

                if (result.Succeeded)
                {
                    UserProfile userProfile = new UserProfile();
                    var bitMapImg = imgService.HttpPostedFileBaseToBitmap(register.avatar);
                    var newImg = imgService.CreateImage(bitMapImg, 32, 32);
                    userProfile.AvatarPath = await imgService.Upload(newImg, register.Email, register.avatar.FileName);
                    userProfile.AvatarLink = await imgService.SharedFile(userProfile.AvatarPath);
                    userProfile.Name = register.Name;
                    userProfile.SurName = register.SurName;
                    userProfile.Id = user.Id;

                    userProfileRepository.Create(userProfile);
                    userProfileRepository.SaveChanges();
                    userManager.AddToRole(user.Id, "User");
                    unitOfWork.CommitTransaction();

                    return StatusAccountViewModel.Success;
                }
            }
            catch
            {
               await imgService.DeleteFile("/" + register.Email);
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
