using System.Data;
using System.Threading.Tasks;
using Enterprise.Services.Providers;
using Enterprise.Overspesification.Services;
using ProjectBase.Data;
using ProjectBase.Utils;
using ProjectBase.ErrorHandle;

namespace Enterprise.Services.Common
{
    public class CatalogServiceDataSet:ICatalogDataSetService
    {
        public async Task<DataSet> GetOverdueOrders()
        {
            var res = await ExceptionManager.Process(
                () => DataSetServiceProvider.DoGetOverdueOrders(),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            return res;
        }

        public DataSet GetOverdueOrdersSynchron()
        {
            var res = DataSetServiceProvider.DoGetOverdueOrdersSynchron();         
            return res;
        }

        public DataSet GetAvaliableBooksSynchron()
        {
            var res = DataSetServiceProvider.DoGetAvaliableBooksSynchron();
            return res;
        }
    }
}
