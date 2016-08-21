using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Web;
using System.Runtime.Remoting.Messaging;
using ProjectBase.Utils;
using NHibernate.Search.Store;
using NHibernate.Event;
using NHibernate.Search.Event;
using NHibernate.Search;

namespace ProjectBase.Data
{
    /// <summary>
    /// Handles creation and managment of session and transactions
    /// This implement also meets the criteria of thread and lazy
    /// </summary>
    public sealed class NHibernateSessionManager
    {
        public static NHibernateSessionManager Instance
        {
            get
            {
                return Nested.NHibernateSessionManager;
            }
        }

        /// <summary>
        /// private static constructor to enforce singleton
        /// </summary>
        private NHibernateSessionManager()
        {
            InitSessionFactory();
        }

        /// <summary>
        /// Assists with ensuring and thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            internal static readonly NHibernateSessionManager NHibernateSessionManager = new NHibernateSessionManager();
            static Nested() { }
        }

        private void InitSessionFactory()
        {
            var cfg = new Configuration().Configure();
            //new SchemaExport(cfg).Execute(true, true, false); 
            
            sessionFactory = cfg.BuildSessionFactory();
        }

        public ISession GetSession()
        {
            ISession session = ContextSession;
            if (session == null)
            {
                session = sessionFactory.OpenSession();
                ContextSession = session;
            }
            Check.Ensure(session != null, "session was not be null");

            return session;
        }

        public void CloseSession()
        {
            ISession session = ContextSession;
            if (session != null && session.IsOpen)
            {
                session.Flush();
                session.Close();
            }
            ContextSession = null;
        }

        public void BeginTransaction()
        {
            ITransaction transaction = ContextTransaction;
            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                ContextTransaction = transaction;
            }
        }

        public void CommitTransaction()
        {
            ITransaction transaction = ContextTransaction;
            try 
            {
                if (HasOpenTransaction())
                {
                    transaction.Commit();
                    ContextTransaction = null;
                }
            }
            catch (HibernateException)
            {
                RollBackTransaction();
                throw;
            }
        }

        public void RollBackTransaction()
        {
            ITransaction transaction = ContextTransaction;
            try 
            {
                if (HasOpenTransaction())
                {
                    transaction.Rollback();
                }
                ContextTransaction = null;
            }
            finally
            {
                CloseSession();
            }
        }
        public bool HasOpenTransaction()
        {
            ITransaction transaction = ContextTransaction;
            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        private bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        private ISession ContextSession
        {
            get 
            {
                if (IsInWebContext())
                {
                    return (ISession)HttpContext.Current.Items[SESSIONKEY];
                }
                else
                {
                    return (ISession)CallContext.GetData(SESSIONKEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[SESSIONKEY] = value;
                }
                else
                {
                    CallContext.SetData(SESSIONKEY, value);
                }
            }
        }
        private ITransaction ContextTransaction
        {
            get 
            {
                if (IsInWebContext())
                {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTIONKEY];
                }
                else
                {
                    return (ITransaction)CallContext.GetData(TRANSACTIONKEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[TRANSACTIONKEY] = value;
                }
                else
                {
                    CallContext.SetData(TRANSACTIONKEY, value);
                }
            }
        }

        private ISessionFactory sessionFactory;
        private string TRANSACTIONKEY = "TRANSACTIONKEY";
        private string SESSIONKEY = "SESSIONKEY";
    }
}
