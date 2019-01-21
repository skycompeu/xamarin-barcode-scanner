using OmniReader.Core.EMDK;
using OmniReader.GUI.ViewModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        private LoginViewModel ViewModel;

        public LoginView ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = ViewModel = new LoginViewModel() { Navigation = Navigation };
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IAppService>().FinishAffinity();

            //var existingPages = Navigation.NavigationStack.ToList();
            //foreach (var page in existingPages)
            //{
            //    Navigation.RemovePage(page);
            //}

            return base.OnBackButtonPressed();
        }
    }
}