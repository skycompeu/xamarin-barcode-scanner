using OmniReader.GUI.ViewModel.Config.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.Device
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceConfigView : ContentPage
	{
        private DeviceViewModel ViewModel;

        public DeviceConfigView ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new DeviceViewModel() { Navigation = Navigation };
        }
    }
}