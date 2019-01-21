using OmniReader.GUI.ViewModel.Config.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.Sync
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SyncUserView : ContentPage
	{
        private SyncUserViewModel ViewModel;

        public SyncUserView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new SyncUserViewModel() { Navigation = Navigation };
        }
    }
}