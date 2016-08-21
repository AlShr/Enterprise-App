using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Enterprise.Web;
using Ninject;
using Enterprise.CoreData.DataInterfaces;
using Enterprise.Data;


namespace Enterprise.Web
{
    public class BaseWebService : WebService
    {
        
        /// <summary>
        /// Exposes accessor for the <see cref="IDaoFactory" /> used by all pages.
        /// Obtain a references to the component by using type of serice contract.
        /// </summary>
        public IDaoFactory DaoFactory
        {
            get 
            {
                return (IDaoFactory)CustomHttpApplication.AppKernel.Get<NHibernateDaoFactory>();
            }
        }

    }
}