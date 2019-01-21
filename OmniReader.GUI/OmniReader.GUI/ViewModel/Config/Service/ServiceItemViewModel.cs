using FluentValidation.Results;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.Data.Database.Validator;
using OmniReader.Data.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Config.Service
{
    public class ServiceItemViewModel : BaseViewModel
    {
        public AdditionalService Service { get; set; }
        private Repository<AdditionalService> Database;
        private ObservableCollection<ValidationFailure> m_ValidationErrors;

        public ObservableCollection<ValidationFailure> ValidationErrors
        {
            get => m_ValidationErrors;
            set => SetProperty(ref m_ValidationErrors, value);
        }
        
        public ICommand SaveCommand { get; set; }

        public ServiceItemViewModel(AdditionalService service = null)
        {
            service = service == null ? new AdditionalService() { UniqueId = 0, Active = true, ModifiedAt = DateTime.Now } : service;
            Service = service;
            Setup();
            SetupCommands();
        }

        private void Setup()
        {
            Title = PrepareTitle();
            Database = new Repository<AdditionalService>();
            ValidationErrors = new ObservableCollection<ValidationFailure>();
        }

        private string PrepareTitle()
        {
            return (Service.UniqueId == 0) ? "Dodawanie" : "Edycja";
        }

        private void SetupCommands()
        {
            SaveCommand = new Command(Save);
        }

        private void Save()
        {
            var validator = new AdditionalServiceValidator();
            ValidationResult results = validator.Validate(Service);

            bool success = results.IsValid;
            if (!success)
            {
                ValidationErrors = new ObservableCollection<ValidationFailure>(results.Errors);
                return;
            }

            try
            {
                if (Service != null) Service.ModifiedAt = DateTime.Now;
                if (Service.UniqueId == 0) {
                    Service.UniqueId = DatabaseHelper.GenerateUniqueId();
                    Database.Insert(Service);
                };

                if (Service.UniqueId > 0) Database.Update(Service);
                
                Alert("Sukces", "Zapisano pomyślnie", "Ok");
            }
            catch (Exception e)
            {
                Alert("Błąd krytyczny", e.Message, "Ok");
            }
            
        }
    }
}
