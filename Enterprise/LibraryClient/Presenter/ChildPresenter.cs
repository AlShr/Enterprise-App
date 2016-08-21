using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.ServiceModel;
using Enterprise.Model;
using Enterprise.Overspesification.Services;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using ProjectBase.Utils;
using ProjectBase.ErrorHandle;
using ProjectBase.Data;



namespace LibraryClient.Presenter
{
    public class ChildPresenter : BasePresenter<IChildView>, IDisposable
    {
        public ChildPresenter(IApplicationController controller, IChildView view, ICatalogServiceObject service,
            IReaderCartServiceObject readerservice)
            : base(controller, view)
        {
            this.service = service;
            this.view = view;
            this.readerservice = readerservice;
            
            Check.Ensure(service != null, "CatalogServiceObject must be provided");
            Check.Ensure(readerservice != null, "ReaderCartService must be provided");

            this.view.CatalogManager.NewRowObjectEdit += RunPublisherDialogForm;
            this.view.CatalogManager.NewRowObjectSubViewEdit += RunAuthorDialogForm;
            this.view.CatalogManager.SearchedFilterChanged += SearchedBookChange;
            this.view.CatalogManager.NewRowObjectChanged += UpdateRowObject;
            this.view.CatalogManager.SettedPaginationChanged += SettedPaginationChange;
            this.view.PubCatalogManager.NewRowObjectChanged += UpdateRowObject;
            this.view.AuthCatalogManager.NewRowObjectChanged += UpdateRowObject;
            this.view.CatalogManager.NewRowObjectAdded += SetRowObject;
            this.view.CatalogManager.NewRowObjectCellEdit += UpdateRowObjectMainView;
            this.view.CheckedCatalogChanged += CheckedCatalogChange;
            
            this.view.SelectCurrent += SelectedRowChange;
            this.view.EditCurrent += EditedRowChanged;
            this.view.SubViewEditCurrent += EditedRowChangedSubView;
            this.view.NewChildObjectAdded += NewRowObjectAdded;
            this.view.CheckedItemsInventory += CheckItemsToInventory;
            this.view.GoToOrderedInventory += GoToOrderedInventory;
            this.view.GoToOrderReport += GoToOrderedReport;
           
            #region Select Currnet UserData

            this.view.SelectCurrentUser += SelectedRowUserChanged;
            this.view.CatalogUserManager.NewRowObjectSelect += ViewSelectedRowChanged;
            #endregion
            InitViewCatalogData(0, 200);              
        }

        public override void Run()
        {            
            View.Show();
        }

        private void RunPublisherDialogForm(object sender, NewRowObjectArgs<BookModel> arg)
        {
            this.book = arg.RowObject as BookModel;
            Controller.Run<PublisherDialogPresenter, BookModel>(book);
            view.CatalogManager.SimulateNewSelectRow(book);
        }

        private void RunAuthorDialogForm(object sender, NewRowObjectArgs<BookModel> arg)
        {
            this.book = arg.RowObject as BookModel;
            int indexselectRow = arg.RowObjectHandle;
            Controller.Run<AuthorDialogPresenter, BookModel, int>(book, indexselectRow);
            view.CatalogManager.SimulateNewSelectRow(book);
        }

        private List<long> SelectListItems()
        {
            IList<PublisherModel> publishers = view.MasterDetailData;
            List<long> bookIds = new List<long>();
            for (int i = 0; i < publishers.Count; i++)
            {
                publishers[i].Books.ForEach(delegate(BookModel book)
                {
                    if (book.IsCheked == true)
                    {
                        bookIds.Add(book.ID);
                    }
                });
            }
            return bookIds;
        }

        private async void CheckItemsToInventory()
        {           
            try
            {
                long[] indexes = await readerservice.SetReaderCartSelections(readerId, SelectListItems().ToArray());
                Controller.Run<ReaderCartPresenter, long[], long>(indexes, readerId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        private void GoToOrderedInventory()
        {
            Controller.Run<InventoryPresenter, long>(readerId);
        }

        private void GoToOrderedReport()
        {
            Controller.Run<ReportPreviewPresenter>();
        }

        public async void UpdateRowObject(object sender, NewRowObjectArgs<BookModel> arg)
        {
            this.book = arg.RowObject as BookModel;
            try 
            {
                await service.SetBookToAuthorRelations(book);
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }           
        }

        public async void UpdateRowObject(object sender, NewRowObjectArgs<PublisherModel> arg)
        {
            try
            {
                this.publisher = arg.RowObject as PublisherModel;
                bool flag = await service.UpdatePublisher(publisher);
                MessageBox.Show(flag == true ? "Success Update" : "Sorry not save changes");

            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        public async void UpdateRowObject(object sender, NewRowObjectArgs<AuthorModel> arg)
        {
            try
            {
                this.author = arg.RowObject as AuthorModel;
                bool flag = await service.UpdateAuthor(author);
                MessageBox.Show(flag == true ? "Sucess Update" : "Sorry not save changes");
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        public async void UpdateRowObjectMainView(object sender, NewRowObjectCellEditArgs<BookModel, long> arg)
        {
            this.book = arg.RowObject as BookModel;
            long publisherId = arg.CellEdit;
            PublisherModel res = await service.GetPublisherBy(publisherId);
            if (publisher != null)
            {
                this.book.Publisher = res;
                try
                {
                    await service.SetBookToAuthorRelations(book);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ShowMessage(ex);
                }              
            }
        }

        public async void SetRowObject(object sender, NewRowObjectArgs<BookModel> arg)
        {
            try
            {
                this.book = arg.RowObject as BookModel;
                await service.SetBookToAuthorRelations(book);
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

      
        public void LoadingDataChanged(object sender, EventArgs e)
        {
            View.OnLoadingDataChanged();
        }

        public void NewRowObjectAdded()
        {
            View.CatalogManager.OnSelectedRowObjectNewChildAdded();
        }

        public void SelectedRowChange()
        {
            View.CatalogManager.OnSelectedRowChanged();
        }

        public void EditedRowChanged()
        {
            View.CatalogManager.OnEditedRowChanged();
        }

        public void EditedRowChangedSubView()
        {
            View.CatalogManager.OnSubViewEditedRowChanged();
        }

        #region Select Currnet User Data

        public void ViewSelectedRowChanged(object sender, NewRowObjectArgs<long> e)
        {
            readerId = e.RowObject;
        }

        private void SelectedRowUserChanged()
        {
            View.CatalogUserManager.OnSelectedRowChanged();
        }
        #endregion

        #region fire event searchlookup value changed  or fire event checkbox checked change

        public void SearchedBookChange(object sender, NewSearchedFilterArgs e)
        {
            SearchModel searchModel = e.SearchModel as SearchModel;
            InitViewCatalogData(searchModel);
        }

        public void CheckedCatalogChange(object sender, EventArgs e)
        {
            InitViewCatalogData(0, 200);
        }

        public void ViewCatalogLoad(object sender, EventArgs e)
        {
            InitViewCatalogData(0, 200);
        }

        public void SettedPaginationChange(object sender, NewSettedPaginationArgs e)
        {
            PageSelector selector = e.PageSelector as PageSelector;
            InitViewCatalogData(selector.CurrentPage, selector.PageSize);
        }

        #endregion      
 
        #region Action


        private void InitViewCatalogData(int pageNumber, int pageSize)
        {
            try
            {
                InitCatalogData(pageNumber, pageSize);
                InitUserLookUpData();
                //await InitLookUpData();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }                  
        }

        public async void InitViewCatalogData(SearchModel searchmodel)
        {
            try
            {
                await InitSearchCatalogData(searchmodel);
            }
            catch (Exception ex)
            {              
                ExceptionManager.ShowMessage(ex);
            }
        }
        #endregion

        #region Process Data      

        //private async Task InitLookUpData()
        //{
        //    IList<PublisherModel> publishers = await service.GetCatalogPublishers();
        //    IList<BookModel> books = await service.GetCatalogBooks();
        //    IList<AuthorModel> authors = await service.GetCatalogAuthors();
        //    this.view.InitServiceDataSearchLookUp(publishers, books, authors);
        //}   

        private void InitUserLookUpData()
        {
            view.ReaderCatalog = service.GetCatalogReaders();
            view.BindServiceDataLookUpEditUserData();
        }

        private void InitCatalogData(int pageNumber, int pageSize)
        {
            bookToauthor =  service.GetRelationsBookToAuthor(pageNumber, pageSize);
            CatalogDataBinding(bookToauthor);
        }


        public void CatalogDataBinding(IList<BookToAuthorModel> bookToauthor)
        {
            var publisherBindList = new BindingList<PublisherModel>();
            service.CatalogData(bookToauthor);
            view.MasterDetailData = service.LibraryDataParser.PublisherModel;
            view.BindServiceData();
            view.InitServiceDataSearchLookUp(
                service.LibraryDataParser.PublisherModel,
                service.LibraryDataParser.BookLookUp,
                service.LibraryDataParser.AuthorLookUp);

            //view.BookCatalog = service.BookLookUp;
            //view.PublisherCatalog = service.PublisherlookUp;
            //view.BindServiceData();
            //view.InitServiceDataSearchLookUp(view.PublisherCatalog, view.BookCatalog, null);
        }

        private async Task InitSearchCatalogData(SearchModel searchmodel)
        {
            bookToauthor = await service.GetRelationsBookToAuthor(searchmodel);
            CatalogDataBinding(bookToauthor);
        }    
   
        #endregion

      

        public void Dispose()
        {
            this.view.CatalogManager.NewRowObjectEdit -= RunPublisherDialogForm;
            this.view.CatalogManager.NewRowObjectSubViewEdit -= RunAuthorDialogForm;
            this.view.CatalogManager.SearchedFilterChanged -= SearchedBookChange;
            this.view.CatalogManager.NewRowObjectChanged -= UpdateRowObject;
            this.view.CatalogManager.NewRowObjectCellEdit -= UpdateRowObjectMainView;
            this.view.CheckedCatalogChanged -= CheckedCatalogChange;
            this.view.SelectCurrent -= SelectedRowChange;
            this.view.EditCurrent -= EditedRowChanged;
            this.view.SubViewEditCurrent -= EditedRowChangedSubView;
            this.view.NewChildObjectAdded -= NewRowObjectAdded;
            this.view.CheckedItemsInventory -= CheckItemsToInventory;
        }

        private BookModel book;
        private PublisherModel publisher;
        private AuthorModel author;
        private long readerId;
        private readonly ICatalogServiceObject service;
        private readonly IReaderCartServiceObject readerservice;
        private IList<BookToAuthorModel> bookToauthor;
        private IChildView view;
    }
}
