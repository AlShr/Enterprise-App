using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Enterprise.Web.DbProvider;
using ProjectBase.Utils;
using ProjectBase.Data;


namespace Enterprise.Web
{
    /// <summary>
    /// Summary description for DataSetService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataSetService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet GetOverdueRecoveringOrders()
        {
            try 
            {
                return context.ApprovedReportNotRecovered();
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }
        }

        [WebMethod]
        public DataSet GetAvaliableBooks()
        {
            try 
            {
                return context.AvaliableBooks();
            }
            catch (Exception ex)
            {
                SoapException se = new SoapException("Fault", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, ex);
                Logger.Instance.Error(se);
                throw se;
            }
        }


        private DbContext context = new DbContext();
    }
}
