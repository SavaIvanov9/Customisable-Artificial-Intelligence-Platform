namespace CAI.Data.Repositories.Abstraction
{
    using Data.Abstraction;
    using Models.Abstraction;
    using System.Linq;

    public abstract class DataRepository<T> : AuditableRepository<T>, IDataRepository<T> where T : class, IDataModel
    {
        protected DataRepository(ICaiDbContext context) : base(context)
        {
        }

        public T FindById(long id, bool isDeleted = false)
        {
            return this.Set.FirstOrDefault(x => x.Id == id && x.IsDeleted == isDeleted);
        }
    }
}
