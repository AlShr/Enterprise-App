using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enterprise.CoreData.DataInterfaces;
using Ninject.Modules;
using Enterprise.Data;

namespace Enterprise.Web
{
    public class RepoContainerNinjectModule:NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDaoFactory>().To<NHibernateDaoFactory>();
        }
    }
}