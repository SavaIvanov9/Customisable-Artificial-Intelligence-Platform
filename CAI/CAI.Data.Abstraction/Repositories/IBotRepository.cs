namespace CAI.Data.Abstraction.Repositories
{
    using System.Collections.Generic;
    using Filtering;
    using Models;

    public interface IBotRepository : IDataRepository<Bot>
    {
        //IEnumerable<Bot> FindByUser(string userId, BotFilter filter = null);

        IEnumerable<Bot> AllContainingInName(string query, BotFilter filter = null);

        Bot FindFirstByFilter(BotFilter filter);

        IEnumerable<Bot> FindAllByFilter(BotFilter filter);
    }
}
