namespace CAI.Data.Abstraction.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected GenericRepository(ICaiDbContext context)
        {
            this.Context = context;
            this.Set = context.Set<T>();
        }

        protected ICaiDbContext Context { get; set; }

        protected IDbSet<T> Set { get; set; }

        public virtual IEnumerable<T> All()
        {
            return this.Set.AsEnumerable();
        }

        public virtual void Add(T entity)
        {
        //    var entry = this.Context.Entry(entity);
        //    if (entry.State == EntityState.Detached)
        //    {
        //        this.Set.Attach(entity);
        //    }

        //    entry.State = EntityState.Added;

            this.ChangeEntityState(entity, EntityState.Added);
        }

        public virtual void Update(T entity)
        {
            //var entry = this.Context.Entry(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    this.Set.Attach(entity);
            //}

            //entry.State = EntityState.Modified;

            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            //var entry = this.Context.Entry(entity);
            //if (entry.State == EntityState.Detached)
            //{
            //    this.Set.Attach(entity);
            //}

            //entry.State = EntityState.Deleted;

            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        //public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        //{
        //    return this.Set.Where(predicate)
        //        .AsEnumerable();
        //}

        public void Detach(T entity)
        {
            //var entry = this.Context.Entry(entity);
            //entry.State = EntityState.Detached;

            this.ChangeEntityState(entity, EntityState.Detached);
        }

        protected void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.Set.Attach(entity);
            }

            entry.State = state;
        }
    }
}