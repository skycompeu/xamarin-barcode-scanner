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
	public partial class RaportPreviewView : ContentPage
	{
        private RaportPreviewViewModel viewModel;
        private string m_Date;
		public RaportPreviewView(string date)
		{
            m_Date = date;
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = null;
            BindingContext = viewModel = new RaportPreviewViewModel(m_Date) { Navigation = Navigation};
        }
    }
}