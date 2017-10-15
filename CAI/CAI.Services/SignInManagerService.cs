namespace CAI.Services
{
    using Abstraction;
    using Data.Models;
    using Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.User;
    using System.Threading.Tasks;

    public class SignInManagerService : ISignInManagerService
    {
        public async Task<SignInStatus> PasswordSignInAsync(ApplicationSignInManager signInManager,
            string email, string password, bool rememberMe, bool shouldLockout)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);

            return result;
        }

        public async Task<bool> HasBeenVerifiedAsync(ApplicationSignInManager signInManager)
        {
            return await signInManager.HasBeenVerifiedAsync();
        }

        public async Task<SignInStatus> TwoFactorSignInAsync(ApplicationSignInManager signInManager,
            string provider, string code, bool rememberMe,
            bool rememberBrowser)
        {
            var result = await signInManager.TwoFactorSignInAsync(provider, code, rememberMe, rememberBrowser);

            return result;
        }

        public async Task SignInAsync(ApplicationSignInManager signInManager, UserViewModel userModel, bool isPersistent,
            bool rememberBrowser)
        {
            var user = new User { UserName = userModel.Email, Email = userModel.Email };
            await signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public async Task<string> GetVerifiedUserIdAsync(ApplicationSignInManager signInManager)
        {
            var userId = await signInManager.GetVerifiedUserIdAsync();

            return userId;
        }

        public async Task<bool> SendTwoFactorCodeAsync(ApplicationSignInManager signInManager, string selectedProvider)
        {
            var result = await signInManager.SendTwoFactorCodeAsync(selectedProvider);

            return result;
        }

        public async Task<SignInStatus> ExternalSignInAsync(ApplicationSignInManager signInManager, ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var result = await signInManager.ExternalSignInAsync(loginInfo, isPersistent);

            return result;
        }

        public void DisposeSignInManager(ApplicationSignInManager signInManager)
        {
            signInManager.Dispose();
        }
    }
}
