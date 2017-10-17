namespace CAI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Abstraction;

    public class User : IdentityUser, IAuditableModel
    {
        private ICollection<Bot> _bots;

        public User()
        {
            this._bots = new HashSet<Bot>();
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
        }

        public User(string username) : base(username)
        {
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

        public ICollection<Bot> Bots
        {
            get => this._bots;
            set => this._bots = value;
        }
    }
}
