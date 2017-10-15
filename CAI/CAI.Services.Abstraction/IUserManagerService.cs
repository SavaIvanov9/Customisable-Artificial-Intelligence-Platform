namespace CAI.Services.Abstraction
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Models.User;

    public interface IUserManagerService
    {
        Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel, string password);

        Task<IdentityResult> CreateAsync(ApplicationUserManager userManager, UserViewModel userModel);

        Task<UserViewModel> FindByNameAsync(ApplicationUserManager userManager, string userName);

        Task<bool> IsEmailConfirmedAsync(ApplicationUserManager userManager, string id);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUserManager userManager, string id,
            string code, string password);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUserManager userManager,
            string userId, string code);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUserManager userManager, string userId);

        void DisposeUserManager(ApplicationUserManager userManager);

        Task<IdentityResult> AddLoginAsync(ApplicationUserManager userManager, string id, UserLoginInfo login);

        Task<string> GetPhoneNumberAsync(ApplicationUserManager userManager, string userId);

        Task<bool> GetTwoFactorEnabledAsync(ApplicationUserManager userManager, string userId);

        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUserManager userManager, string userId);

        Task<IdentityResult> RemvoveLoginAsync(ApplicationUserManager userManager, string userId, UserLoginInfo login);

        Task SignInAsync(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId);

        Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUserManager userManager, string userId, string number);

        Task SendSmsWithSecurityCode(ApplicationUserManager userManager, string userId, string number);

        Task EnableTwoFactorAuthentication(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId);

        Task DisableTwoFactorAuthentication(ApplicationUserManager userManager, ApplicationSignInManager signInManager, string userId);

        Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUserManager userManager, string userId, string phoneNumber, string code);

        Task<IdentityResult> SetPhoneNumberAsync(ApplicationUserManager userManager, string userId);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUserManager userManager, string userId, string oldPassword, string newPassword);

        Task<IdentityResult> AddPasswordAsync(ApplicationUserManager userManager, string userId, string newPassword);

        Task<bool> DoesUserExists(ApplicationUserManager userManager, string userId);

        Task<string> GetUserPasswordHash(ApplicationUserManager userManager, string userId);

        void Dispose(ApplicationUserManager userManager);
    }
}
