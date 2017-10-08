namespace CAI.Data.Abstraction.Repositories
{
    using System.Collections.Generic;
    using Filtering;
    using Models;

    public interface IActivationKeyRepository : IDataRepository<ActivationKey>
    {
        IEnumerable<ActivationKey> FindAllByNames(string[] names, bool isDeleted = false);

        ////IEnumerable<Bot> AllContainingInName(string query, bool isDeleted = false);
        //IEnumerable<Bot> AllContainingInName(string query, BotFilter filter = null);

        //Bot FindFirstByFilter(BotFilter filter);

        //IEnumerable<Bot> FindAllByFilter(BotFilter filter);
    }
}
