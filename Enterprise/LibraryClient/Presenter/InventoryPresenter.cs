using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using Enterprise.CoreData.Domain;
using Enterprise.Model;
using ProjectBase.Data;
using ProjectBase.ErrorHandle;
using ProjectBase.Utils;
using Enterprise.Overspesification.Services;
using LibraryClient.Common;
using LibraryClient.Views;
using LibraryClient.Managers;



namespace LibraryClient.Presenter
{
    public class InventoryPresenter : BasePresenter<IInventoryView, long>,IDisposable
    {
        public InventoryPresenter(IApplicationController controller, IInventoryView view, ICatalogServiceObject service, IReaderCartServiceObject readerservice)
            : base(controller, view)
        {           
            this.service = service;
            this.readerservice = readerservice;
            this.view = view;
            Check.Ensure(service != null, "ICatalogServiceObject must be provided");
            Check.Ensure(readerservice != null, "IReaderCartServiceObject must be provided");
            Check.Ensure(view != null, "IInventoryView must be provided");
            Check.Ensure(controller != null, "IApplicationController must be provided");

            View.CloseShowDialog += () => CloseDialogShow();           
            this.view.CatalogManager.NewRowObjectCellEdit += UpdateRowObject;        
        }

        public async void UpdateRowObject(object sender, NewRowObjectCellEditArgs<BookModel, long> e)
        {
            BookModel rowobject = e.RowObject as BookModel;
            long approvedOrderId = e.CellEdit;
            await readerservice.SetOrderedItemRecoveryDate(rowobject.ID, approvedOrderId, rowobject.RecoveredDate);      
        }

        private void CloseDialogShow()
        {
            View.Close();
        }      

        public override void Run(long argument2)
        {
            this.readerId = argument2;
            InitViewData(this.readerId);
            View.Show();
        }

        public async void InitViewData(long readerId)
        {
            try 
            {
                approvedOrders = await readerservice.GetApprovedOrderByReader(readerId);
                CatalogDataBinding(approvedOrders);          
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }     
        }

        public async void CatalogDataBinding(IList<ApprovedOrderModel> approvedorders)
        {
            try 
            {
                view.MasterData = readerservice.GetOrderedBookModel(approvedorders);
                view.PublisherCatalog = await service.GetCatalogPublishers();
                view.BindServiceData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ShowMessage(ex);
            }          
        }

        public void Dispose()
        {
            this.view.CatalogManager.NewRowObjectCellEdit -= UpdateRowObject;   
        }
        private IList<ApprovedOrderModel> approvedOrders;      
        private long readerId;
        private readonly ICatalogServiceObject service;
        private readonly IReaderCartServiceObject readerservice;
        private IInventoryView view;
    }
}
