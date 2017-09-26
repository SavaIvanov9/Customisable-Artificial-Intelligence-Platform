namespace CAI.Data.Repositories
{
    using CAI.Data.Abstraction;
    using CAI.Data.Abstraction.Repositories;
    using CAI.Data.Models;

    class BotRepository : GenericRepository<Bot>, IRepository<Bot>
    {
        public BotRepository(ICaiDbContext context)
            : base(context)
        {
        }
    }
}