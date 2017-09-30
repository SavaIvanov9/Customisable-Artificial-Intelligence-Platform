namespace CAI.Data
{
    using CAI.Data.Abstraction;
    using CAI.Data.Abstraction.Repositories;
    using CAI.Data.Models;
    using CAI.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Abstraction;
    using Repositories.Abstraction;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICaiDbContext _context;
        private readonly IDictionary<Type, object> _repositories;

        public UnitOfWork() : this(new CaiDbContext())
        {
        }

        public UnitOfWork(ICaiDbContext context)
        {
            this._context = context;
            this._repositories = new Dictionary<Type, object>();
        }

        public IBotRepository BotRepository => (BotRepository)this.GetRepository<Bot>();

        //public BotGenericRepository BotGenericRepository => (BotGenericRepository)this.GetRepository<Bot>();
        
        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }

        private IDataRepository<T> GetRepository<T>() where T : class, IDataModel
        {
            var repositoryType = typeof(T);

            if (!this._repositories.ContainsKey(repositoryType))
            {
                var type = typeof(DataRepository<T>);

                this.SetType(repositoryType, ref type);

                this._repositories.Add(repositoryType, Activator.CreateInstance(type, this._context));
            }

            return (IDataRepository<T>)this._repositories[repositoryType];
        }

        private void SetType(Type repositoryType, ref Type type)
        {
            if (repositoryType.IsAssignableFrom(typeof(Bot)))
                type = typeof(BotRepository);
            //else if (repositoryType.IsAssignableFrom(typeof(Position)))
            //    type = typeof(PositionRepository);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
