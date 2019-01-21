using OmniReader.GUI.View.Config;
using OmniReader.GUI.View.Report;
using OmniReader.GUI.View.Scan;
using OmniReader.GUI.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{
        private ScanHome ScanHomeView;
        private ConfigHomeView ConfigHomeView;
        private ReportHomeView ReportHomeView;
        private HomeViewModel ViewModel;
        
        public HomeView ()
		{
            InitializeComponent();
            Init();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new HomeViewModel() { Navigation = Navigation };
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void Init() {
            ScanHomeView = new ScanHome();
            ConfigHomeView = new ConfigHomeView();
            ReportHomeView = new ReportHomeView();
        }
        
        private async void Btn_MenuScan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(ScanHomeView, false);
        }

        private async void Btn_MenuReport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(ReportHomeView, false);
        }

        private async void Btn_MenuConfig_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(ConfigHomeView, false);
        }
    }
}