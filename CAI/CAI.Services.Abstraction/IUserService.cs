namespace CAI.Services.Abstraction
{
    using System.Collections.Generic;
    using Models.User;

    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsers();

        UserViewModel FindUser(string id);

        bool EditUser(UserViewModel model, string modifiedBy);

        bool DeleteUser(string id, string deletedBy);
    }
}
