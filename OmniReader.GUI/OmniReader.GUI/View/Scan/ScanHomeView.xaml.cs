using OmniReader.GUI.ViewModel.Scan;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanHome : ContentPage
	{
        private ScannerView ScanSingleView;
        private ScannerView ScanMultiView;
        private ScanServiceHome ScanServiceHome;

        /* ScanHomeViewModel */
        private ScanHomeViewModel ScanHomeViewModel;
        
        public ScanHome ()
		{
			InitializeComponent ();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ScanHomeViewModel = new ScanHomeViewModel();
        }
        
        // ------------------------------------------------------------------------------
        //jump directly to ScannerView
        private async void Btn_NavigateSingle_Clicked(object sender, EventArgs e)
        {
            ScanSingleView = null;
            ScanSingleView = new ScannerView(Core.Scanner.ScanType.SingleParcel, null);
            await Navigation.PushAsync(ScanSingleView, false);
        }

        private async void Btn_NavigateMulti_Clicked(object sender, EventArgs e)
        {
            ScanMultiView = null;
            ScanMultiView = new ScannerView(Core.Scanner.ScanType.GroupParcel, null);
            await Navigation.PushAsync(ScanMultiView, false);
        }
        
        // ------------------------------------------------------------------------------
        //jump to ScanServiceHome
        private async void Btn_NavigateService_Clicked(object sender, EventArgs e)
        {
            ScanServiceHome = null;
            ScanServiceHome = new ScanServiceHome();
            await Navigation.PushAsync(ScanServiceHome, false);
        }
    }
}
 