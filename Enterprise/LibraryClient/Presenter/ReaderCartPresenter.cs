using System;
using System.Threading.Tasks;
using Enterprise.Model;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using Enterprise.Overspesification.Services;
using ProjectBase.Data;
using ProjectBase.ErrorHandle;
using ProjectBase.Utils;


namespace LibraryClient.Presenter
{
    public class ReaderCartPresenter : BasePresenter<IReaderCartView, long[], long>,IDisposable
    {
        public ReaderCartPresenter(IApplicationController controller, IReaderCartView view, ICatalogServiceObject service, IReaderCartServiceObject readerservice)
            : base(controller, view)
        {
            this.service = service;
            this.readerservice = readerservice;
            this.view = view;
            Check.Ensure(controller != null, "IApplicationController must be provided");
            Check.Ensure(service != null, "ICatalogService must be proivided");
            Check.Ensure(readerservice != null, "IReaderCartService must be provided");           
            View.CloseShowDialog += () => CloseDialogShow();                
            this.view.CatalogManager.NewRowObjectSelect += ViewSelectedRowChanged;
            this.view.Order += OrderedReadingCart;

        }

        private void CloseDialogShow()
        {
            View.Close();
        }

        public void ViewSelectedRowChanged(object sender, NewRowObjectArgs<ItemModel> e)
        {
            ItemModel item = e.RowObject as ItemModel;          
            Task t = item.IsReadCarted == false ? readerservice.RemoveReaderCartSelection(readerId, item.BookId, item.ReaderCartSelection.ID) :
                readerservice.SetReaderCartSelection(this.readerId, item.BookId);
        }     

        public void OrderedReadingCart()
        {
            readerservice.OrderedReadingCartSelections(this.readerId);

        }

        public override void Run(long[] argument1, long argument2)
        {
            this.indexes = argument1;
            this.readerId = argument2;
            InitViewData(indexes);
            View.Show();
        }

        public async void InitViewData(long[] Ids)
        {
            try
            {
                view.BookCatalog = await service.GetCatalogBooks();
                view.MasterDetailData = Ids == null ? await service.GetItems(Ids, readerId) :
                    await service.GetItems(readerId);
                view.BindServiceData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }       
        }

        public void Dispose()
        {
            this.view.CatalogManager.NewRowObjectSelect -= ViewSelectedRowChanged;
            this.view.Order -= OrderedReadingCart;
        }

        private ICatalogServiceObject service;
        private IReaderCartServiceObject readerservice;
        private IReaderCartView view;
        private long readerId;
        private long[] indexes;
    }
}
