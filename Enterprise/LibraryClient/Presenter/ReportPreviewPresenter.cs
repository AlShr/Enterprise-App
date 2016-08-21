using LibraryClient.Common;
using LibraryClient.Views;

namespace LibraryClient.Presenter
{
    public class ReportPreviewPresenter:BasePresenter<IReportPreview>
    {
        public ReportPreviewPresenter(IApplicationController controller, IReportPreview view)
            : base(controller, view)
        {
            this.view = view;
            View.CloseShowDialog += () => CloseDialogShow();
        }

        public override void Run()
        {
            View.Show();
        }

        private void CloseDialogShow()
        {
            View.Close();
        }

        private IReportPreview view;        
    }
}
