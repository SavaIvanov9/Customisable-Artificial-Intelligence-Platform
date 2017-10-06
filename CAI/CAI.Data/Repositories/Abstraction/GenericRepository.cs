namespace CAI.Data.Repositories.Abstraction
{
    using Data.Abstraction;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

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
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public virtual void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        //public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        //{
        //    return this.Set.Where(predicate)
        //        .AsEnumerable();
        //}

        public void Detach(T entity)
        {
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

        protected IQueryable<T> ApplyFilter(IQueryable<T> query, object filter = null)
        {
            if (filter != null)
            {
                var props = filter.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    var value = prop.GetValue(filter);

                    if (value != null)
                    {
                        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                        Expression left = Expression.Property(parameter, typeof(T).GetProperty(prop.Name));
                        Expression right = Expression.Constant(value);
                        Expression predicateBody = Expression.Equal(left, right);
                        query = query.Where(Expression.Lambda<Func<T, bool>>(predicateBody, new ParameterExpression[] { parameter }));
                    }
                }
            }

            return query;
        }
    }
}