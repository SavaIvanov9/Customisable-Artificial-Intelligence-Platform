namespace CAI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Data.Models;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models.User;

    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnitOfWork data) : base(data)
        {
        }

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

        public async Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel, string password)
        {
            var user = new User { UserName = userModel.Email, Email = userModel.Email };
            var result = await userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel)
        {
            var user = new User { UserName = userModel.Email, Email = userModel.Email };
            var result = await userManager.CreateAsync(user);

            return result;
        }

        public async Task SignInAsync(ApplicationSignInManager signInManager, UserViewModel userModel, bool isPersistent,
            bool rememberBrowser)
        {
            var user = new User { UserName = userModel.Email, Email = userModel.Email };
            await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

        public async Task<UserViewModel> FindByNameAsync(ApplicationUserManager userManager, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            return new UserViewModel() { Id = user.Id, UserName = user.UserName, Email = user.Email };
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUserManager userManager, string id)
        {
            var result = await userManager.IsEmailConfirmedAsync(id);

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUserManager userManager, string id, string code, string password)
        {
            var result = await userManager.ResetPasswordAsync(id, code, password);

            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUserManager userManager, string userId, string code)
        {
            var result = await userManager.ConfirmEmailAsync(userId, code);

            return result;
        }

        public async Task<string> GetVerifiedUserIdAsync(ApplicationSignInManager signInManager)
        {
            var userId = await signInManager.GetVerifiedUserIdAsync();

            return userId;
        }

        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager userManager, string userId)
        {
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(userId);

            return userFactors;
        }

        public async Task<bool> SendTwoFactorCodeAsync(ApplicationSignInManager signInManager, string selectedProvider)
        {
            var result = await signInManager.SendTwoFactorCodeAsync(selectedProvider);

            return result;
        }

        public async Task<SignInStatus> ExternalSignInAsync(ApplicationSignInManager signInManager, ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var result = await signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            return result;
        }

        public void DisposeUserManager(ApplicationUserManager userManager)
        {
            userManager.Dispose();
        }

        public void DisposeSignInManager(ApplicationSignInManager signInManager)
        {
            signInManager.Dispose();
        }

        public async Task<IdentityResult> AddLoginAsync(ApplicationUserManager userManager, string id, UserLoginInfo login)
        {
            var result = await userManager.AddLoginAsync(id, login);
            return result;
        }
    }
}
