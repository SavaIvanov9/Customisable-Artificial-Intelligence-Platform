namespace CAI.Data
{
    using CAI.Data.Abstraction;
    using CAI.Data.Abstraction.Repositories;
    using CAI.Data.Models;
    using CAI.Data.Repositories;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICaiDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public UnitOfWork() : this(new CaiDbContext())
        {
        }

        public UnitOfWork(ICaiDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Bot> BotRepository => (BotRepository)this.GetRepository<Bot>();

        //public BotRepository BotRepository => (BotRepository)this.GetRepository<Bot>();


        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var repositoryType = typeof(T);

            if (!this.repositories.ContainsKey(repositoryType))
            {
                var type = typeof(GenericRepository<T>);

                this.SetType(repositoryType, ref type);

                this.repositories.Add(repositoryType, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[repositoryType];
        }

        private void SetType(Type repositoryType, ref Type type)
        {
            if (repositoryType.IsAssignableFrom(typeof(Bot)))
                type = typeof(BotRepository);
            //else if (repositoryType.IsAssignableFrom(typeof(Position)))
            //    type = typeof(PositionRepository);
        }
    }
}
