using OmniReader.GUI.View.Config.Device;
using OmniReader.GUI.View.Config.Service;
using OmniReader.GUI.View.Config.User;
using OmniReader.GUI.ViewModel.Config;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfigHomeView : ContentPage
	{
        private ConfigHomeViewModel ViewModel;

        public ConfigHomeView ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new ConfigHomeViewModel();
        }

        private void Btn_UserHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserHomeView(), false);
        }
        
        private void Btn_ServiceHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ServiceHomeView(), false);
        }
        
        private void Btn_DeviceHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DeviceConfigView(), false);
        }
    }
}