namespace CAI.Data.Repositories
{
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;

    public class ActivationKeyRepository : DataRepository<ActivationKey>, IActivationKeyRepository
    {
        public ActivationKeyRepository(ICaiDbContext context)
            : base(context)
        {
        }
    }
}

