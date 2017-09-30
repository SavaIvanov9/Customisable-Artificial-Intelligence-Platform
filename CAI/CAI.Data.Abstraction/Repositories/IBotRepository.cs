namespace CAI.Data.Abstraction.Repositories
{
    using System.Collections.Generic;
    using Models;

    public interface IBotRepository : IDataRepository<Bot>
    {
        IEnumerable<Bot> AllContainingInName(string query, bool isDeleted = false);

        Bot FindByName(string name, bool isDeleted = false);
    }
}
