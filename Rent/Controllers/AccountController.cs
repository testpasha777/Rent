using BLL.Interface;
using BLL.Services;
using BLL.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Rent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService = new AccountIdentityService();

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
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if(ModelState.IsValid)
            {
                var info = accountService.GetExternalLoginInfo();

                if(info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var result = accountService.CreateLogin(model.Email);

                if(result == StatusAccountViewModel.Success)
                {
                    return RedirectToLocal(returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        #region AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult Login(LoginViewModel model)
        {
            string json = "";
            int rez = 0;
            string message = "";

            if (ModelState.IsValid)
            {
                var status = accountService.Login(model);

                if (status == StatusAccountViewModel.Success)
                {
                    message = "Усе добре";
                    rez = 1;
                }
                else
                    message = "Uncorrected data!";
            }

            json = JsonConvert.SerializeObject(new
            {
                rez = rez,
                message = message
            });

            return Content(json, "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ContentResult Register(RegisterViewModel registerVM)
        {
            string json = "";
            int res = 0;
            string message = "";

            if(ModelState.IsValid)
            {
                var status = accountService.Register(registerVM);

                if(status == StatusAccountViewModel.Success)
                {
                    message = "Усе добре";
                    res = 1;

                    LoginViewModel login = new LoginViewModel
                    {
                        Email = registerVM.Email,
                        Password = registerVM.Password,
                        IsRememberme = false,
                    };

                    accountService.Login(login);
                }
                else if(status == StatusAccountViewModel.Dublication)
                {
                    message = "Error. Dublication!";
                    res = 2;
                }
                else
                {
                    message = "Uncorrected data!";
                }
            }

            json = JsonConvert.SerializeObject(new
            {
                res = res,
                message = message
            });

            return Content(json, "application/json");
        }

        #endregion

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