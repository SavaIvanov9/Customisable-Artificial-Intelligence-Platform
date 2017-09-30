namespace CAI.Data.Repositories.Abstraction
{
    using Data.Abstraction;
    using Models.Abstraction;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class DataRepository<T> : GenericRepository<T>, IDataRepository<T> where T : class, IDataModel
    {
        protected DataRepository(ICaiDbContext context) : base(context)
        {
        }

        public override IEnumerable<T> All()
        {
            return base.Set.Where(x => !x.IsDeleted).AsEnumerable();
        }

        public override void Add(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            //this.ValidateAuditingUser(entity.ModifiedBy);
            //this.auditChecker.CheckAndAudit(entity, audit);
            base.Add(entity);
        }

        public override void Update(T entity)
        {
            entity.ModifiedOn = DateTime.Now;
            //this.ValidateAuditingUser(entity.ModifiedBy);
            //this.auditChecker.CheckAndAudit(entity, audit);
            base.Update(entity);
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            //this.ValidateAuditingUser(entity.DeletedBy);
            this.Update(entity);
        }

        public T FindById(long id, bool isDeleted = false)
        {
            return this.Set.FirstOrDefault(x => x.Id == id && x.IsDeleted == isDeleted);
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
