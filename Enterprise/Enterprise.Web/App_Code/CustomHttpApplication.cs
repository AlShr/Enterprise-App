using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;


namespace Enterprise.Web
{
    public class CustomHttpApplication : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            
        }
        public void Application_Error(object sender, EventArgs e) { }

        public void Session_Start(object sender, EventArgs e) { }

        public void Session_End(object sender, EventArgs e)
        {
        }

        public void Application_End(object sender, EventArgs e)
        {
            appKernel.Dispose();
        }

        public static IKernel AppKernel
        {
            get 
            {
                if (appKernel == null)
                {
                    appKernel = new StandardKernel(new RepoContainerNinjectModule());
                }
                return appKernel; 
            }
        }

        private static IKernel appKernel;
        
    }
}