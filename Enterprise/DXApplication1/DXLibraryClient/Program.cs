using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DXLibraryClient.Views;
using Enterprise.Model;
using Enterprise.Overspesification.Services;
using Enterprise.Services.Common;
using LibraryClient.Presenter;
using LibraryClient.Common;
using LibraryClient.Managers;
using LibraryClient.Views;


namespace DXLibraryClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            var controller = new ApplicationController(new LigthInjectAdapter())
            .RegisterView<IChildView, ChildForm>()
            .RegisterView<IInventoryView, InventoryForm>()
            .RegisterView<IReaderCartView, ReaderCartForm>()
            .RegisterView<IReportPreview, ReportPreviewForm>()
            .RegisterView<IShowDialogResult<AuthorModel, long>, DialogEditorForm<AuthorModel, long>>()
            .RegisterView<IShowDialogResult<PublisherModel, long>, DialogEditorForm<PublisherModel, long>>()
            .RegisterService<ICatalogServiceObject, CatalogServiceObject>()
            .RegisterService<IReaderCartServiceObject, ReaderCartServiceObject>()
            .RegisterService<ICatalogDataSetService, CatalogServiceDataSet>()
            .RegisterService<ICatalogManager<BookModel>, CatalogManager<BookModel>>()
            .RegisterService<ICatalogManager<AuthorModel>, CatalogManager<AuthorModel>>()
            .RegisterService<ICatalogManager<PublisherModel>, CatalogManager<PublisherModel>>()
            .RegisterService<ICatalogManager<long>, CatalogManager<long>>()
            .RegisterService<ICatalogManager<ItemModel>,CatalogManager<ItemModel>>()
            .RegisterInstance(new ApplicationContext());
            controller.Run<ChildPresenter>();
        }
    }
}
