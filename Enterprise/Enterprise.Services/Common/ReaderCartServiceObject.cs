using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Enterprise.Services.WebServices;
using Enterprise.CoreData.Domain;
using Enterprise.Model;
using Enterprise.CoreData.LuceneIndex;
using Enterprise.Services.Converters;
using Enterprise.Services.Providers;
using Enterprise.Services.ServiceLocator;
using ProjectBase.Data;
using ProjectBase.Utils;
using ProjectBase.ErrorHandle;
using Enterprise.Overspesification.Services;



namespace Enterprise.Services.Common
{
    public class ReaderCartServiceObject : IReaderCartServiceObject
    {
        public async Task<long[]> SetReaderCartSelections(long readerId, long[] bookIds)
        {
            long[] indexes = await ExceptionManager.Process(
                () => ObjectServiceProvider.DoSetReaderCartSelection(readerId, bookIds),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            return indexes;
        }

        public async Task SetReaderCartSelection(long readerId, long bookId)
        {
            await ExceptionManager.Process(
                () => ObjectServiceProvider.DoSetReaderCartSelection(readerId, bookId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task RemoveReaderCartSelection(long readerId, long bookId, long readercartselectionId)
        {
            await ExceptionManager.Process(
                () => ObjectServiceProvider.DoRemoveReaderCartSelection(readerId, bookId, readercartselectionId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task OrderedReadingCartSelections(long readerId)
        {
            await ExceptionManager.Process(
                () => ObjectServiceProvider.DoOrderedReaderCartSelection(readerId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task SetOrderedItemRecoveryDate(long bookId, long approvedorderId, DateTime date)
        {
            await ExceptionManager.Process(
                () => ObjectServiceProvider.DoSetOrderItemRecoveryDate(bookId, approvedorderId, date),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
        }

        public async Task<List<ApprovedOrderModel>> GetApprovedOrderByReader(long readerId)
        {
            IList<ApprovedOrderDto> approvedOrderDtos = await ExceptionManager.Process(
                () => ObjectServiceProvider.DoGetApprovedOrdersByReader(readerId),
                ExceptionManager.IsFatal,
                ex => Logger.Instance.Error(ex));
            List<ApprovedOrderModel> approvedOrders = approvedOrderDtos.Select<ApprovedOrderDto, ApprovedOrderModel>(
                x => ConverterFactory.GetConverter<ApprovedOrderModel, ApprovedOrderDto>().ToRepoItem(x, true)).ToList();
            return approvedOrders;
        }

        public IList<OrderedBookModel> GetOrderedBookModel(IList<ApprovedOrderModel> approvedOrders)
        {
            foreach (var apprOrder in approvedOrders)
            {
                OrderedBookModel orderedBook = new OrderedBookModel();
                orderedBook.ID = apprOrder.ID;
                orderedBook.OrderDate = apprOrder.OrderedDate;
                orderedBook.RecoveredDate = apprOrder.RecoveredDate;
                orderedBook.OrderNumber = apprOrder.ApprovedNumber;
                orderedBook.Items = GetBookItems(apprOrder);
                orderedBookCatalog.Add(orderedBook);
            }
            return orderedBookCatalog;
        }

        public IDictionary<BookModel, List<AuthorModel>> InitMaster(IList<BookToAuthorModel> bookToauthor)
        {
            if (bookToauthor != null)
            {
                //bookcatalog = catalogService.InitBookAuthorDetail(bookToauthor);
            }
            return bookcatalog;
        }


        private List<BookModel> GetBookItems(ApprovedOrderModel apprOrder)
        {
            List<BookModel> bookItems = new List<BookModel>();
            foreach (var item in apprOrder.OrderItems)
            {
                var book = item.Book;
                book.RecoveredDate = item.RecoveredDate;
                book.PlanedRecoveringDate = item.PlanedRecoveringDate;
                bookItems.Add(book);

            }
            return bookItems;
        }

        public ICatalogServiceObject CatalogService
        {
            get
            {
                if (catalogService == null)
                {
                    catalogService = ServiceLocatorNinject.AppKernel.Get<CatalogServiceObject>();
                }
                return catalogService;
            }
            set
            {
                catalogService = value;
            }
        }

        public IDictionary<BookModel, List<AuthorModel>> BookCatalog
        {
            get { return bookcatalog; }
            set { bookcatalog = value; }
        }

        private IDictionary<BookModel, List<AuthorModel>> bookcatalog = new Dictionary<BookModel, List<AuthorModel>>();
        private List<OrderedBookModel> orderedBookCatalog = new List<OrderedBookModel>();
        private List<BookToAuthorModel> bookToauthor = new List<BookToAuthorModel>();
        private ICatalogServiceObject catalogService;

    }
}
