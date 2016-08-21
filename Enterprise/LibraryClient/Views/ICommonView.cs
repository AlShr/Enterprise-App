using System;
using System.Data;
using System.Collections.Generic;
using LibraryClient.Managers;
using LibraryClient.Presenter;
using LibraryClient.Common;
using Enterprise.Model;



namespace LibraryClient.Views
{

    public interface IInventoryView : IView
    {
        ICatalogManager<BookModel> CatalogManager { get; }
        IList<ItemModel> MasterDetailData { get; set; }
        IList<OrderedBookModel> MasterData { get; set; }
        IList<PublisherModel> PublisherCatalog { get; set; }
        IList<BookModel> BookCatalog { get; set; }
        void BindServiceData();        
        event Action CloseShowDialog;
    }

    public interface IChildView : IView
    {
        ICatalogManager<BookModel> CatalogManager { get; }
        ICatalogManager<PublisherModel> PubCatalogManager { get; }
        ICatalogManager<AuthorModel> AuthCatalogManager { get; }
        ICatalogManager<long> CatalogUserManager { get; }
        IList<PublisherModel> MasterDetailData { get; set; }
        IList<PublisherModel> PublisherCatalog { get; set; }
        IList<AuthorModel> AuthorCatalog { get; set; }
        IList<ReaderModel> ReaderCatalog { get; set; }
        IList<BookModel> BookCatalog { get; set; }
        PageSelector PageSelector { get; set; }
        object SelectRowObject { get; set; }
        void VisibleEditLookUpState(BookModel book);
        void BindServiceData();
        void BindServiceDataLookUpEditUserData();
        void InitServiceDataSearchLookUp(IList<PublisherModel> publishers, 
            IList<BookModel> books, IList<AuthorModel> authors);
        void OnLoadingDataChanged();
        void ResetUi();
        event EventHandler CheckedCatalogChanged;
        event EventHandler Loading;
        event Action CheckedItemsInventory;
        event Action NewChildObjectAdded;
        event Action Save;
        event Action EditCurrent;
        event Action SubViewEditCurrent;
        event Action SelectCurrent;
        event Action SelectCurrentUser;
        event Action GoToOrderedInventory;
        event Action GoToOrderReport;
    }


    public interface IReaderCartView : IView
    {
        ICatalogManager<ItemModel> CatalogManager { get; }
        IList<ItemModel> MasterDetailData { get; set; }
        IList<BookModel> BookCatalog { get; set; }
        void BindServiceData();
        event Action Order;
        event Action CloseShowDialog;
    }

   

    public interface IGridControlBase : IView
    {
        void BindServiceData();
        void OnSelectedRowChanged();
        event Action SelectCurrent;
        event EventHandler SelectedRowChanged;
        event Action CloseShowDialog;
    }


    public interface IShowDialogResult<T, TId> : IGridControlBase
        where T : BaseModel<TId>
    {
        IList<T> BindCollection { get; set; }
        event EventHandler<NewRowObjectArgs<T>> NewRowObjectSelect;
        ICatalogManager<T> CatalogManager { get; }
    }

    public interface ILookUpControl
    {
        void BindServiceData(); 
     
    }

    public interface IGuiFactory
    {
        ILookUpControl CreateLookUpControl();
    }

    public class GuiFactory<TLookUpControl> : IGuiFactory
        where TLookUpControl : ILookUpControl, new()       
    {
        public virtual ILookUpControl CreateLookUpControl() { return new TLookUpControl(); }     
    }
    
    public interface ISearchLookUpView : ISubView, ILookUpControl
    {
        IList<PublisherModel> PublisherCatalog { get; set; }
        IList<AuthorModel> AuthorCatalog { get; set; }
        IList<BookModel> BookCatalog { get; set; }
    }

    public interface IReportControl
    {
        void BindServiceData();
    }

    public interface IReportFactory
    {
        IReportControl CreateReportControl();
        IReportControl CreateReportControl1();
    }

    public class ReportFactory<TReportControl, TReportControl1> : IReportFactory
        where TReportControl : IReportControl, new()
        where TReportControl1 : IReportControl, new()
    {
        public virtual IReportControl CreateReportControl() { return new TReportControl(); }
        public virtual IReportControl CreateReportControl1() { return new TReportControl1(); }
    }

    public interface IReportView : ISubView, IReportControl
    {
        DataSet ReportData { get; set; }
    }

    public interface IReportPreview : IView
    {
        event Action CloseShowDialog;
    }
}
