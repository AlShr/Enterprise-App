using System;
using System.Web;
using ProjectBase.Data;
using Ninject;
using log4net;

namespace Enterprise.Web
{
    /// <summary>
    /// Implements the Open-Session-In-View pattern using <see cref="NHibernateSessionManager"/>.
    /// Assumes that each HTTP request is given a single transaction for the entire page-lifecycle.
    /// </summary>
    public class NHibernateSessionModule:IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(BeginTransaction);
            context.EndRequest += new EventHandler(CommitAndCloseSession);
        }

        /// <summary>
        /// Opens a session within a transaction at the beginning of the HTTP request.
        /// This doesn`t actually open a connectin to the database until needed.
        /// </summary>
        public void BeginTransaction(object sender, EventArgs e)
        {           
            NHibernateSessionManager.Instance.BeginTransaction();
        }

        /// <summary>
        /// Commits and closses the NHibernate session provided by supplied <see cref="NHibernateSessionManager"/>.
        /// Assumes a transaction was begun at the beginning of the request; but a transaction or session does 
        /// not have to be opened for thid operate successfully.
        /// </summary>
        public void CommitAndCloseSession(object sender, EventArgs e)
        {
            try
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSession();               
            }           
        }

        public void Dispose() { }
    }
}