using OmniReader.GUI.ViewModel.Report;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace OmniReader.GUI.View.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportSyncView : ContentPage
	{
        private ReportSyncViewModel ViewModel;
        private Data.App.Model.Report m_Report;

        public ReportSyncView (Data.App.Model.Report report)
		{
            m_Report = report;
            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new ReportSyncViewModel(m_Report) { Navigation = Navigation };
        }
    }
}