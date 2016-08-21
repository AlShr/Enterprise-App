
using DXLibraryClient.Views;
using LibraryClient.Common;
using LibraryClient.Views;

namespace DXLibraryClient.Common
{
  
    public class SearchLookUpGuiFactory : GuiFactory<SearchLookUpEditorForm>
    {
        public new ILookUpControl CreateLookUpControl() { return new SearchLookUpEditorForm(); }
    }

    public class ReportPreviewFactory : ReportFactory<OverdueRecoverOrderView, AvaliableBookView>
    {
        public new IReportControl CreateReportControl() { return new OverdueRecoverOrderView(); }
        public new IReportControl CreateReportControl1() { return new AvaliableBookView(); }
    }

}
