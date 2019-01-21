using OmniReader.Data.Database.Model;
using OmniReader.GUI.ViewModel.Scan;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanServiceHome : ContentPage
	{
        private ScanHomeServiceViewModel ViewModel;

        public ScanServiceHome ()
		{
			InitializeComponent ();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new ScanHomeServiceViewModel { Navigation = Navigation };
        }

        private void LV_ServiceList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is AdditionalService item)) return;
            Navigation.PushAsync(page: new ScannerView(Core.Scanner.ScanType.SingleParcelWithService, e.Item as AdditionalService));
            LV_ServiceList.SelectedItem = null;
        }
    }
}