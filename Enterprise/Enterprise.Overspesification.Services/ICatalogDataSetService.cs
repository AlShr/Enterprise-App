using System;
using System.Data;
using System.Threading.Tasks;

namespace Enterprise.Overspesification.Services
{
    public interface ICatalogDataSetService
    {
        Task<DataSet> GetOverdueOrders();
        DataSet GetOverdueOrdersSynchron();
        DataSet GetAvaliableBooksSynchron();
    }
}
