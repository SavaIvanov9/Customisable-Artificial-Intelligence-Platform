namespace CAI.Data.Abstraction.Repositories
{
    using Filtering;
    using Models;
    using System.Collections.Generic;

    public interface IIntentionRepository : IDataRepository<Intention>
    {
        ////IEnumerable<Bot> AllContainingInName(string query, bool isDeleted = false);
        //IEnumerable<Bot> AllContainingInName(string query, BotFilter filter = null);

        //Bot FindFirstByFilter(BotFilter filter);

        //IEnumerable<Bot> FindAllByFilter(BotFilter filter);
    }
}
