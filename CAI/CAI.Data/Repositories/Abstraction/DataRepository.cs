using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI.Data.Repositories.Abstraction
{
    using System.Data.Entity;
    using Data.Abstraction;
    using Data.Abstraction.Repositories;
    using Models.Abstraction;

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
