using OmniReader.Data.Database.Model;
using OmniReader.GUI.ViewModel.Config.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Config.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserItemView : ContentPage
	{

        private UserItemViewModel ViewModel;

        //AppUser
        public UserItemView (AppUser appUser = null)
		{
			InitializeComponent ();
            BindingContext = ViewModel = new UserItemViewModel(appUser) { Navigation = Navigation };
		}
	}
}