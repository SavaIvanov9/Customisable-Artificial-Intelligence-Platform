namespace CAI.Data.Repositories
{
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;

    public class IntentionRepository : DataRepository<Intention>, IIntentionRepository
    {
        public IntentionRepository(ICaiDbContext context)
            : base(context)
        {
        }
    }
}
