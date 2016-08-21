using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Naskar.QueryOverSpec;
using NHibernate;

namespace ProjectBase.Data
{
    public interface IDao<T, TId> where T : Entity<TId>
    {
        IList<T> Get();
        IList<T> Get(Expression<Func<T, bool>> filter);
        IList<T> Get(int pageNumber, int pageSize);
        IList<T> Get(int pageNumber, int pageSize, Expression<Func<T, object>> filter);
        T GetBy(Expression<Func<T, bool>> filter);
        IList<T> Find(Action<IQueryOver<T, T>> builder);
        IList<T> Find(Action<IQueryOver<T, T>> builder, int pageNumber, int pageSize);
        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> filter);
        void CommitChanges();
    }
}
