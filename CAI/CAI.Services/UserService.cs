namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Models.User;

    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork data) : base(data)
        {
        }

        public UserViewModel FindUser(string id)
        {
            var user = this.FindUserById(id);

            return new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                PasswordHash = user.PasswordHash,
                IsDeleted = user.IsDeleted,
                DeletedOn = user.DeletedOn
            };
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return this.Data.UserRepository
                .AllWithDeleted()
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn,
                    ModifiedOn = u.ModifiedOn,
                    PasswordHash = u.PasswordHash,
                    IsDeleted = u.IsDeleted,
                    DeletedOn = u.DeletedOn
                });
        }

        public bool EditUser(UserViewModel model, string modifiedBy)
        {
            var user = this.FindUserById(model.Id);

            user.UserName = model.UserName;
            user.IsDeleted = model.IsDeleted;
            user.Email = model.Email;
            user.PasswordHash = model.PasswordHash;
            user.ModifiedBy = modifiedBy;

            this.Data.UserRepository.Update(user);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool DeleteUser(string id, string deletedBy)
        {
            var user = this.FindUserById(id);

            user.DeletedBy = deletedBy;
            this.Data.UserRepository.HardDelete(user);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }
    }
}
