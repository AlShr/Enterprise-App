using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Naskar.QueryOverSpec;
using Naskar.QueryOverSpec.Impl;
using NHibernate;
using NHibernate.Search;
using ProjectBase.Utils;

namespace ProjectBase.Data
{

    public abstract class AbstractNHibernateDao<T, TId> : IDao<T, TId> where T : Entity<TId>
    {
        public IList<T> Get(int pageNumber, int pageSize, Expression<Func<T, object>> filter)
        {
            IList<T> entities = NHibernateSession.QueryOver<T>().OrderBy(filter).Asc.Skip(pageNumber * pageSize).Take(pageSize).List();
            return entities;
        }

        public IList<T> Get(int pageNumber, int pageSize)
        {
            IList<T> entities = NHibernateSession.QueryOver<T>().Skip(pageNumber * pageSize).Take(pageSize).List();
            return entities;
        }

        public IList<T> Get(Expression<Func<T, bool>> filter)
        {
            IList<T> entities = NHibernateSession.QueryOver<T>().Where(filter).List<T>();
            return entities;
        }

        public IList<T> Get()
        {
            IList<T> entities = NHibernateSession.QueryOver<T>().List<T>();
            return entities;
        }

        public T GetBy(Expression<Func<T, bool>> filter)
        {
            T entity = NHibernateSession.QueryOver<T>().Where(filter).SingleOrDefault<T>();
            return entity;
        }

        public IList<T> Find(Action<IQueryOver<T, T>> builder, int pageNumber, int pageSize)
        {
            var query = NHibernateSession.QueryOver<T>();
            builder(query);
            var list = query.Skip(pageNumber * pageSize).Take(pageSize).List();
            return list;
        }

        public IList<T> Find(Action<IQueryOver<T, T>> builder)
        {
            var query = NHibernateSession.QueryOver<T>();
            builder(query);
            var list = query.List();
            return list;
        }

        public T Save(T entity)
        {
            NHibernateSession.Save(entity);
            return entity;
        }

        public T SaveOrUpdate(T entity)
        {
            NHibernateSession.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            NHibernateSession.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            IList<T> entities = NHibernateSession.QueryOver<T>().Where(filter).List<T>();
            foreach (T entity in entities)
                NHibernateSession.Delete(entity);
        }

        public void CommitChanges()
        {
            if (NHibernateSessionManager.Instance.HasOpenTransaction())
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            else
            {
                NHibernateSessionManager.Instance.GetSession().Flush();
            }
        }

        public ISession NHibernateSession
        {
            get { return NHibernateSessionManager.Instance.GetSession(); }
        }
    }
}
