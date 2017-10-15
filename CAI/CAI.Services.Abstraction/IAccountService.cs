namespace CAI.Services.Abstraction
{
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.User;

    public interface IAccountService
    {
        Task<SignInStatus> PasswordSignInAsync(ApplicationSignInManager signInManager,
            string email, string password, bool rememberMe, bool shouldLockout);

        Task<bool> HasBeenVerifiedAsync(ApplicationSignInManager signInManager);

        Task<SignInStatus> TwoFactorSignInAsync(ApplicationSignInManager signInManager,
            string provider, string code, bool rememberMe, bool rememberBrowser);

        Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel, string password);

        Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel);

        Task SignInAsync(ApplicationSignInManager signInManager, UserViewModel userModel, bool isPersistent, bool rememberBrowser);

        Task<UserViewModel> FindByNameAsync(ApplicationUserManager userManager, string userName);

        Task<bool> IsEmailConfirmedAsync(ApplicationUserManager userManager, string id);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUserManager userManager, string id,
            string code, string password);
        
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUserManager userManager,
            string userId, string code);

        Task<string> GetVerifiedUserIdAsync(ApplicationSignInManager signInManager);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager userManager, string userId);

        Task<bool> SendTwoFactorCodeAsync(ApplicationSignInManager signInManager, string selectedProvider);

        Task<SignInStatus> ExternalSignInAsync(ApplicationSignInManager signInManager, 
            ExternalLoginInfo loginInfo, bool isPersistent);

        void DisposeUserManager(ApplicationUserManager userManager);

        void DisposeSignInManager(ApplicationSignInManager signInManager);

        Task<IdentityResult> AddLoginAsync(ApplicationUserManager userManager, string id, UserLoginInfo login);
    }
}
