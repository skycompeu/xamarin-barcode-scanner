using OmniReader.GUI.ViewModel.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Report
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportHomeView : ContentPage
	{
        private ReportHomeViewModel ViewModel;

        public ReportHomeView ()
		{
			InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = null;
            BindingContext = ViewModel = new ReportHomeViewModel();
        }

        private async void LV_ReportList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is Data.App.Model.Report item)) return;
            await Navigation.PushAsync(new ReportSyncView(item), false);
            LV_ReportList.SelectedItem = null;
        }

        private async void Btn_ShowRaports_Clicked(object sender, EventArgs e)
        {
            string data = ((Button)sender).BindingContext as string;
            await Navigation.PushAsync(new RaportPreviewView(data), false);
        }
    }
}