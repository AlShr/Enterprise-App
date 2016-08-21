
using Enterprise.Overspesification.Services;
using LibraryClient.Common;
using LibraryClient.Views;
using ProjectBase.Utils;


namespace LibraryClient.Presenter
{
    public class OverdueReportPresenter : Presenter<IReportView>
    {
        public OverdueReportPresenter(IReportView view, ICatalogDataSetService service)
            : base(view)
        {
            Check.Require(view != null, "ReportView must be provided");
            Check.Require(service != null, "ICatalogDataSetService must be provided");
            this.view = view;
            this.service = service;
           
        }

       
        public void InitReportView()
        {
            view.ReportData = service.GetOverdueOrdersSynchron();
            view.BindServiceData();
        }

        private ICatalogDataSetService service;
     
        private IReportView view;

    }
}
