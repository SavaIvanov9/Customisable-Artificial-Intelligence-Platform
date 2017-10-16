namespace CAI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstraction;
    using Base;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Models.User;

    public class UserManagerService : BaseService, IUserManagerService
    {
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

        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager userManager, string userId)
        {
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(userId);

            return userFactors;
        }

        public void DisposeUserManager(ApplicationUserManager userManager)
        {
            userManager.Dispose();
        }

        public async Task<IdentityResult> AddLoginAsync(ApplicationUserManager userManager, string id, UserLoginInfo login)
        {
            var result = await userManager.AddLoginAsync(id, login);
            return result;
        }

        public async Task<string> GetPhoneNumberAsync(ApplicationUserManager userManager, string userId)
        {
            return await userManager.GetPhoneNumberAsync(userId);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(ApplicationUserManager userManager, string userId)
        {
            return await userManager.GetTwoFactorEnabledAsync(userId);

        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUserManager userManager, string userId)
        {
            return await userManager.GetLoginsAsync(userId);
        }

        public async Task<IdentityResult> RemvoveLoginAsync(ApplicationUserManager userManager, string userId, UserLoginInfo login)
        {
            return await userManager.RemoveLoginAsync(userId, login);
        }

        public async Task SignInAsync(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            ////var user = await this._userManagerService.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUserManager userManager, string userId, string number)
        {
            return await userManager.GenerateChangePhoneNumberTokenAsync(userId, number);
        }

        public async Task SendSmsWithSecurityCode(ApplicationUserManager userManager, string code, string number)
        {
            if (userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = number,
                    Body = "Your security code is: " + code
                };
                await userManager.SmsService.SendAsync(message);
            }
        }

        public async Task EnableTwoFactorAuthentication(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId)
        {
            await userManager.SetTwoFactorEnabledAsync(userId, true);
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }

        public async Task DisableTwoFactorAuthentication(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId)
        {
            await userManager.SetTwoFactorEnabledAsync(userId, false);
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }

        public async Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUserManager userManager, string userId, string phoneNumber, string code)
        {
            return await userManager.ChangePhoneNumberAsync(userId, phoneNumber, code);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(ApplicationUserManager userManager, string userId)
        {
            return await userManager.SetPhoneNumberAsync(userId, null);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUserManager userManager, string userId, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public async Task<IdentityResult> AddPasswordAsync(ApplicationUserManager userManager, string userId, string newPassword)
        {
            return await userManager.AddPasswordAsync(userId, newPassword);
        }

        public async Task<bool> DoesUserExists(ApplicationUserManager userManager, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user != null;
        }

        public async Task<string> GetUserPasswordHash(ApplicationUserManager userManager, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user?.PasswordHash;
        }

        //public void AttchRole(User user, UserRoleType role)
        //{
        //    var userStore = new UserStore<User>(context);
        //    var userManager = new UserManager<User>(userStore);
        //    userManager.AddToRole(user.Id, UserRoleType.Admin.ToString());
        //}

        public void Dispose(ApplicationUserManager userManager)
        {
            userManager.Dispose();
        }

        public UserManagerService(IUnitOfWork data) : base(data)
        {
        }
    }
}
