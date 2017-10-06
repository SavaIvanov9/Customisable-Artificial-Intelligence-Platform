namespace CAI.Data.Repositories.Abstraction
{
    using Data.Abstraction;
    using Models.Abstraction;
    using System;
    using System.Collections.Generic;

    public abstract class AuditableRepository<T> : GenericRepository<T>, IAuditableRepository<T> where T : class, IAuditableModel
    {
        protected AuditableRepository(ICaiDbContext context) : base(context)
        {
        }

        public IEnumerable<T> AllWithDeleted()
        {
            return base.All();
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }
    }
}
