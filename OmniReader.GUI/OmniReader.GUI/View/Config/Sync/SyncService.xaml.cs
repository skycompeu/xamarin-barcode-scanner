using OmniReader.GUI.ViewModel.Config.Sync;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.Sync
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SyncService : ContentPage
	{
        private SyncServiceViewModel ViewModel;

        public SyncService ()
		{
			InitializeComponent ();
       }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new SyncServiceViewModel() { Navigation = Navigation };
        }
    }
}