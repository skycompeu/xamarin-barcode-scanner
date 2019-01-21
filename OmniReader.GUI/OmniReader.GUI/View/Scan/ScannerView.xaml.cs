using FreshMvvm;
using OmniReader.Core.EMDK;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.GUI.ViewModel.Scan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OmniReader.GUI.View.Scan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerView : ContentPage
    {
        public ScannerView (Core.Scanner.ScanType scanType, AdditionalService additionalService = null)
		{
            InitializeComponent();

            BindingContext = new ScannerViewModel(scanType, additionalService) { Navigation = Navigation};
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as ScannerViewModel)?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as ScannerViewModel)?.OnDisappearing();
        }
    }
}