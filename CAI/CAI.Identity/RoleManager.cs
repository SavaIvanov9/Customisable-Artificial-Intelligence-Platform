namespace CAI.Identity
{
    using Common.Enums;
    using Data;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleManager
    {
        public static void AttachRole(User user, UserRoleType role)
        {
            var userStore = new UserStore<User>(new CaiDbContext());
            var userManager = new UserManager<User>(userStore);
            userManager.AddToRole(user.Id, role.ToString());
        }
    }
}
