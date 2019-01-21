using OmniReader.Data.Database.Model;
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
	public partial class ServiceItemView : ContentPage
	{
        private ServiceItemViewModel ViewModel;
        private AdditionalService m_AdditionalService;

        public ServiceItemView (AdditionalService service = null)
		{
            m_AdditionalService = service;
            InitializeComponent ();
		}
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;
            BindingContext = ViewModel = new ServiceItemViewModel(m_AdditionalService) { Navigation = Navigation };
        }
    }
}