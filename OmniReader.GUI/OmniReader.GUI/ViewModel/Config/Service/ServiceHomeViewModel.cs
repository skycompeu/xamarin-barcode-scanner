using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OmniReader.GUI.ViewModel.Config.Service
{
    class ServiceHomeViewModel : BaseViewModel
    {
        Repository<AdditionalService> Database;

        private ObservableCollection<AdditionalService> m_ServiceList;
        public ObservableCollection<AdditionalService> ServiceList
        {
            get => m_ServiceList;
            set => SetProperty(ref m_ServiceList, value);
        }

        public ServiceHomeViewModel()
        {
            Setup();
        }
        
        private void Setup()
        {
            Title = "Usługi";
            Database = new Repository<AdditionalService>();
            ServiceList = new ObservableCollection<AdditionalService>(Database.GetAll());
        }
    }
}
