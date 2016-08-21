using System;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using Enterprise.Model;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;
using Enterprise.Overspesification.Services;
using ProjectBase.ErrorHandle;


namespace LibraryClient.Presenter
{
    public class PublisherDialogPresenter : BasePresenter<IShowDialogResult<PublisherModel, long>, BookModel>, IDisposable
    {
        public PublisherDialogPresenter(IApplicationController controller, IShowDialogResult<PublisherModel, long> view, ICatalogServiceObject service)
            : base(controller, view)
        {

            View.CloseShowDialog += () => CloseDialogShow();
            this.service = service;
            this.view = view;
            this.view.SelectCurrent += SelectedRowChanged;
            this.view.NewRowObjectSelect += SelectRowObject;
            this.view.CatalogManager.NewRowObjectEdit += UpdateRowObject;
            InitViewData();
        }

        private void ChangePublisher(PublisherModel argument)
        {
            book.Publisher = argument;
        }

        private void CloseDialogShow()
        {
            View.Close();
        }

        public override void Run(BookModel argument)
        {
            book = argument;
            View.Show();
        }

        public async void InitViewData()
        {
            try
            {
                view.BindCollection = await service.GetCatalogPublishers();
                view.BindServiceData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        protected void SelectedRowChanged()
        {
            View.OnSelectedRowChanged();
        }

        protected void SelectRowObject(object sender, NewRowObjectArgs<PublisherModel> e)
        {
            PublisherModel selectpublisher = e.RowObject as PublisherModel;
            if (selectpublisher != null)
            {
                book.Publisher = selectpublisher;
            }            
            View.Close();
        }

        public async void UpdateRowObject(object sender, NewRowObjectArgs<PublisherModel> e)
        {
            try
            {
                PublisherModel publisher = e.RowObject as PublisherModel;
                var flag = await service.UpdatePublisher(publisher);
                MessageBox.Show(flag == true ? "Success Update" : "Sorry not save changes");
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        public void Dispose()
        {
            this.view.SelectCurrent -= SelectedRowChanged;
            this.view.NewRowObjectSelect -= SelectRowObject;
            this.view.CatalogManager.NewRowObjectEdit -= UpdateRowObject;
        }

        private BookModel book;
        private ICatalogServiceObject service;
        private IShowDialogResult<PublisherModel, long> view;
    }
}
