namespace CAI.Data.Repositories
{
    using System;
    using Abstraction;
    using CAI.Data.Models;
    using Data.Abstraction.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;
    using Filtering;

    public class BotRepository : DataRepository<Bot>, IBotRepository
    {
        public BotRepository(ICaiDbContext context)
            : base(context)
        {
        }

        //public IEnumerable<Bot> FindByUser(string userId, BotFilter filter = null)
        //{
        //    var botsQuery = this.Set
        //        .Where(x => x.);

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