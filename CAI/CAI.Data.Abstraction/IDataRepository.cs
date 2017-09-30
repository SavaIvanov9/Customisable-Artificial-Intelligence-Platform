namespace CAI.Data.Abstraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Abstraction;

    public interface IDataRepository<T> : IGenericRepository<T> where T : class, IDataModel
    {
        T FindById(long id, bool isDeleted = false);

        IEnumerable<T> AllWithDeleted();

        void HardDelete(T entity);
    }
}
