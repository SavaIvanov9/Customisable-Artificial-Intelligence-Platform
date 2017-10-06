namespace CAI.Data.Repositories
{
    using Abstraction;
    using CAI.Data.Models;
    using Data.Abstraction.Repositories;
    using Filtering;
    using System.Collections.Generic;
    using System.Linq;

    public class BotRepository : DataRepository<Bot>, IBotRepository
    {
        public BotRepository(ICaiDbContext context)
            : base(context)
        {
        }

        //public IEnumerable<Bot> AllContainingInName(string query, bool isDeleted = false)
        //{
        //    return this.Set
        //        .Where(x => x.Name.Contains(query) && x.IsDeleted == isDeleted)
        //        .AsEnumerable();
        //}

        //public Bot FindByName(string name, bool isDeleted = false)
        //{
        //    return this.Set
        //        .FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower())
        //            && x.IsDeleted == isDeleted);
        //}

        public IEnumerable<Bot> AllContainingInName(string query, BotFilter filter = null)
        {
            var botsQuery = this.Set
                .Where(x => x.Name.Contains(query));

            botsQuery = this.ApplyFilter(botsQuery, filter);

            return botsQuery.AsEnumerable();
        }

        public Bot FindFirstByFilter(BotFilter filter)
        {
            var botsQuery = this.Set.AsQueryable();

            botsQuery = this.ApplyFilter(botsQuery, filter);

            return botsQuery.FirstOrDefault();
        }

        public IEnumerable<Bot> FindAllByFilter(BotFilter filter)
        {
            var botsQuery = this.Set.AsQueryable();

            botsQuery = this.ApplyFilter(botsQuery, filter);

            return botsQuery.AsEnumerable();
        }
    }
}