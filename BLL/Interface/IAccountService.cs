using BLL.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IAccountService
    {
        Task<StatusAccountViewModel> Register(RegisterViewModel register);
        StatusAccountViewModel Login(LoginViewModel login);
        StatusAccountViewModel CreateLogin(string email, string name, string surName);
        IEnumerable<string> UserRoles(string email);
        IList<string> UserFactors();
        bool SendTwoFactorCode(string provider);
        ExternalLoginInfo GetExternalLoginInfo();
        SignInStatus ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent);
        void Logout();
    }
}
