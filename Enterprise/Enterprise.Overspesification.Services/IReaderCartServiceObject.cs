using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enterprise.Model;


namespace Enterprise.Overspesification.Services
{
    public interface IReaderCartServiceObject
    {
        Task OrderedReadingCartSelections(long readerId);
        Task<long[]> SetReaderCartSelections(long readerId, long[] bookIds);
        Task SetReaderCartSelection(long readerId, long bookId);
        Task RemoveReaderCartSelection(long readerId, long bookId, long readercartselectionId);
        Task<List<ApprovedOrderModel>> GetApprovedOrderByReader(long readerId);
        Task SetOrderedItemRecoveryDate(long bookId, long approvedorderId, DateTime date);
        IList<OrderedBookModel> GetOrderedBookModel(IList<ApprovedOrderModel> approvedOrders);
    }
}
