namespace CAI.Data.Repositories
{
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;
    using System.Linq;

    public class UserRepository : AuditableRepository<User>, IUserRepository
    {
        public UserRepository(ICaiDbContext context)
            : base(context)
        {
        }

        public User FindById(string id, bool isDeleted = false)
        {
            return this.Set.FirstOrDefault(x => x.Id == id && x.IsDeleted == isDeleted);
        }
    }
}