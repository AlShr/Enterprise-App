using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ProjectBase.Utils.Web
{
    public class ErrorModule : IHttpModule
    {
        #region Fields and Properties
        private static readonly ILog logger = LogManager.GetLogger(typeof(ErrorModule));
        #endregion

        #region IHttpModule Members
        public void Init(HttpApplication application)
        {
            application.Error += new EventHandler(ApplicationError);
        }

        public void Dispose() { }

        #endregion

        public void ApplicationError(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;

            Exception exception;
            for (exception = ctx.Server.GetLastError(); exception.InnerException != null; exception = exception.InnerException)
            { }

            if (exception is HttpException && ((HttpException)exception).ErrorCode == 404)
            {
                logger.Warn("A 404 occurred", exception);
            }
            else
            {
                logger.Error("ErrorModule caught an unhandled exception", exception);
            }
        }
    }
}
