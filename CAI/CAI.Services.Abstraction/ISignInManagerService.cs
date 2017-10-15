namespace CAI.Services.Abstraction
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.User;

    public interface ISignInManagerService
    {
        Task<SignInStatus> PasswordSignInAsync(ApplicationSignInManager signInManager,
            string email, string password, bool rememberMe, bool shouldLockout);

        Task<bool> HasBeenVerifiedAsync(ApplicationSignInManager signInManager);

        Task<SignInStatus> TwoFactorSignInAsync(ApplicationSignInManager signInManager,
            string provider, string code, bool rememberMe, bool rememberBrowser);

        Task SignInAsync(ApplicationSignInManager signInManager, UserViewModel userModel, bool isPersistent, bool rememberBrowser);

        Task<string> GetVerifiedUserIdAsync(ApplicationSignInManager signInManager);

        Task<bool> SendTwoFactorCodeAsync(ApplicationSignInManager signInManager, string selectedProvider);

        Task<SignInStatus> ExternalSignInAsync(ApplicationSignInManager signInManager,
            ExternalLoginInfo loginInfo, bool isPersistent);

        void DisposeSignInManager(ApplicationSignInManager signInManager);
    }
}
