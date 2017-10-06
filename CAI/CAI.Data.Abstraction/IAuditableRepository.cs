namespace CAI.Data.Abstraction
{
    using System.Collections.Generic;
    using Models.Abstraction;

    public interface IAuditableRepository<T> : IGenericRepository<T> where T : class, IAuditableModel
    {
        IEnumerable<T> AllWithDeleted();

        void HardDelete(T entity);
    }
}
