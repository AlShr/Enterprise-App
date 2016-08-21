using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using Enterprise.Services.WebServices;


namespace Enterprise.Services.Providers
{
    public sealed class DataSetServiceProvider
    {
        public static DataSetServiceProvider Instance
        {
            get 
            {
                return Nested.DataSetServiceProvider;
            }
        }

        private DataSetServiceProvider()
        { }

        private class Nested
        {
            internal static readonly DataSetServiceProvider DataSetServiceProvider = new DataSetServiceProvider();
            static Nested() { }
        }

        public async static Task<DataSet> DoGetOverdueOrders()
        {
            DataSetService client;
            try 
            {
                client = new DataSetService();
                var res = Task<DataSet>.Factory.FromAsync(client.BeginGetOverdueRecoveringOrders, client.EndGetOverdueRecoveringOrders, null);
                await res;
                return res.Result;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public static DataSet DoGetOverdueOrdersSynchron()
        {
            DataSetService client;
            try 
            {
                client = new DataSetService();
                var res = client.GetOverdueRecoveringOrders();
                return res;
            }
            catch (SoapException)
            {
                throw;
            }
        }

        public static DataSet DoGetAvaliableBooksSynchron()
        {
            DataSetService client;
            try 
            {
                client = new DataSetService();
                var res = client.GetAvaliableBooks();
                return res;
            }
            catch (SoapException)
            {
                throw;
            }
        }
    }
}
