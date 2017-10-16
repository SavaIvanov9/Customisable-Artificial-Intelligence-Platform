namespace CAI.Services.Models.User
{
    using System;
    using Base;

    public class UserViewModel
    { 
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
