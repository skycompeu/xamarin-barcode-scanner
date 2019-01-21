using OmniReader.Data.Database.Model;
using OmniReader.GUI.View.Config.Sync;
using OmniReader.GUI.ViewModel.Config.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.Service
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServiceHomeView : ContentPage
	{
        private ServiceHomeViewModel ViewModel;

        public ServiceHomeView ()
		{
			InitializeComponent ();
		}

        private async void LV_ServiceList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is AdditionalService item)) return;
            await Navigation.PushAsync(new ServiceItemView(item));
            LV_ServiceList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new ServiceHomeViewModel() { Navigation = Navigation };
        }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ServiceItemView(), false);
        }

        private void BtnSync_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SyncService(), false);
        }
    }
}