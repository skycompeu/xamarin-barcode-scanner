using OmniReader.Data.Database.Model;
using OmniReader.GUI.View.Config.Sync;
using OmniReader.GUI.ViewModel.Config.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserHomeView : ContentPage
	{
        private UserHomeViewModel ViewModel;
        
        public UserHomeView ()
		{
			InitializeComponent ();
		}
        
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserItemView(), false);
        }

        private async void LV_UserList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is AppUser item)) return;
            await Navigation.PushAsync(new UserItemView(item), false);
            LV_UserList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new UserHomeViewModel() { Navigation = Navigation };
        }

        private void BtnSync_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SyncUserView(), false);
        }
    }
}