namespace CAI.Data.Abstraction
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T>
    {
        IEnumerable<T> All();

        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Detach(T entity);
    }
}