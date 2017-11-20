using BLL.Interface;
using BLL.Services;
using BLL.ViewModel;
using DAL.Entities.Entities;
using DAL.Interface;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Rent.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private IUserProfileRepository userRep;

        public AccountController(IAccountService _accountService, IUserProfileRepository _userRep)
        {
            accountService = _accountService;
            userRep = _userRep;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var status = accountService.Login(login);

                if (status == StatusAccountViewModel.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(login);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var status = await accountService.Register(register);

                if (status == StatusAccountViewModel.Success)
                {
                    LoginViewModel login = new LoginViewModel
                    {
                        Email = register.Email,
                        Password = register.Password,
                        IsRememberme = false,
                    };

                    //accountService.Login(login);
                    return RedirectToAction("Login", "Account");
                }
                else if (status == StatusAccountViewModel.Dublication)
                {
                    ModelState.AddModelError("", "Dublicate");
                    return View(register);
                }
                else
                {
                    ModelState.AddModelError("", "Error");
                    return View(register);
                }
            }

            return View(register);
        }

        public ActionResult Logout()
        {
            accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { returnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult SendCode(string returnUrl, bool rememberMe)
        {
            var userFactors = accountService.UserFactors();
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendCode(SendCodeViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(!accountService.SendTwoFactorCode(model.SelectedProvider))
            {
                return View("Error");
            }

            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = accountService.GetExternalLoginInfo();

            if(loginInfo == null)
            {
                RedirectToAction("Index");
            }

            var result = accountService.ExternalSignIn(loginInfo, isPersistent: false);

            if(result == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);
            }
            else if(result == SignInStatus.LockedOut)
            {
                return View("Lockout");
            }
            else if(result == SignInStatus.RequiresVerification)
            {
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            }
            else
            {
                var name = loginInfo.ExternalIdentity.Name.Substring(0, loginInfo.ExternalIdentity.Name.IndexOf(" "));
                var surName = loginInfo.ExternalIdentity.Name.Substring(loginInfo.ExternalIdentity.Name.IndexOf(" "));

                accountService.CreateLogin(loginInfo.Email, name, surName);
                return RedirectToAction("Index", "Home");
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if(User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if(ModelState.IsValid)
        //    {
        //        var info = accountService.GetExternalLoginInfo();

        //        if(info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }

        //        var result = accountService.CreateLogin(model.Email);

        //        if(result == StatusAccountViewModel.Success)
        //        {
        //            return RedirectToLocal(returnUrl);
        //        }
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        [ChildActionOnly]
        public ActionResult ShowUserAvatar()
        {
            var user = userRep.Get(User.Identity.GetUserId());
            UserAvatarViewModel avatar = new UserAvatarViewModel();

            if (user.AvatarLink != null)
            {
                avatar.AvatarLink = user.AvatarLink;
            }

            return PartialView("_UserAvatar", avatar);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
    // Used for XSRF protection when adding external logins

}