namespace CAI.Data.Repositories
{
    using Abstraction;
    using CAI.Data.Models;
    using Data.Abstraction.Repositories;
    using System.Collections.Generic;
    using System.Linq;

    public class BotRepository : DataRepository<Bot>, IBotRepository
    {
        public BotRepository(ICaiDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Bot> AllContainingInName(string query)
        {
            return this.Set.Where(x => x.Name.Contains(query)).AsEnumerable();
        }

        public Bot FindByName(string name)
        {
            return this.Set.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
        }
    }
}