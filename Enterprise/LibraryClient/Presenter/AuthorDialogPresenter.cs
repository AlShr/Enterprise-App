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
    public class AuthorDialogPresenter : BasePresenter<IShowDialogResult<AuthorModel, long>, BookModel, int>, IDisposable
    {
        public AuthorDialogPresenter(IApplicationController controller, IShowDialogResult<AuthorModel, long> view,
            ICatalogServiceObject service)
            : base(controller, view)
        {
            View.CloseShowDialog += () => CloseDialogShow();
            this.service = service;
            this.view = view;
            this.view.SelectCurrent += SelectedRowChanged;
            this.view.NewRowObjectSelect += SelectRowObject;
            this.view.CatalogManager.NewRowObjectCellEdit += UpdateRowObject;
            InitViewData();
        }

        private void ChangeAuthor(AuthorModel argument)
        {
            book.Authors.Add(argument);
        }

        private void CloseDialogShow()
        {
            View.Close();
        }

        public override void Run(BookModel argument1, int argument2)
        {
            this.book = argument1;
            this.indexAuthors = argument2;
            View.Show();
        }

        public async void InitViewData()
        {
            try
            {
                view.BindCollection = await service.GetCatalogAuthors();
                view.BindServiceData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }
        }

        public void SelectedRowChanged()
        {
            View.OnSelectedRowChanged();
        }

        protected void SelectRowObject(object sender, NewRowObjectArgs<AuthorModel> e)
        {
            AuthorModel selectauthor = e.RowObject as AuthorModel;
            if (book.Authors.Count == 0)
            {
                book.Authors.Add(selectauthor);
                View.Close();
            }
            book.Authors[indexAuthors] = selectauthor;
            View.Close();
        }

        public async void UpdateRowObject(object sender, NewRowObjectArgs<AuthorModel> e)
        {
            try
            {
                AuthorModel author = e.RowObject as AuthorModel;
                var flag = await service.UpdateAuthor(author);
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
            this.view.CatalogManager.NewRowObjectCellEdit -= UpdateRowObject;
        }

        private BookModel book;
        private int indexAuthors;
        private ICatalogServiceObject service;
        private IShowDialogResult<AuthorModel, long> view;

    }
}
