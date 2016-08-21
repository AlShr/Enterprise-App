
using Enterprise.Overspesification.Services;
using LibraryClient.Common;
using LibraryClient.Views;
using ProjectBase.Utils;

namespace LibraryClient.Presenter
{
    public class AvaliableBookReportPresenter:Presenter<IReportView>
    {
        public AvaliableBookReportPresenter(IReportView view, ICatalogDataSetService catalogservice)
            : base(view)
        {
            Check.Require(view != null, "ReportView must be provided");
            Check.Require(catalogservice != null, "ReportView must be provided");
            this.view = view;
            this.catalogservice = catalogservice;       
        }        

        public void InitReportView()
        {
            view.ReportData = catalogservice.GetAvaliableBooksSynchron();
            view.BindServiceData();
        }

        private ICatalogDataSetService catalogservice;    
        private IReportView view;
    }
}
