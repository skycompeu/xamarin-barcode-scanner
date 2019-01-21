using FluentValidation.Results;
using OmniReader.Core.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using OmniReader.Core.Manager;
using System.Collections.ObjectModel;

namespace OmniReader.GUI.ViewModel.Config.Device
{
    public class DeviceViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        public bool UniqueParcles { get; set; }
        public string DeviceId { get; set; }

        public string ApiEndpoint { get; set; }
        public string ApiToken { get; set; }
        public string ApiUser { get; set; }
        
        private ObservableCollection<ValidationFailure> m_ValidationErrors;
        public ObservableCollection<ValidationFailure> ValidationErrors
        {
            get => m_ValidationErrors;
            set => SetProperty(ref m_ValidationErrors, value);
        }

        public DeviceViewModel()
        {
            Setup();
            SetupCommand();
        }

        private void Setup()
        {
            Title = "Urządzenie";
            UniqueParcles = Convert.ToBoolean(Convert.ToInt16(ConfigManager.UniqueParcles));
            DeviceId = ConfigManager.DeviceId;
            ApiEndpoint = ConfigManager.ApiEndpoint;
            ApiUser = ConfigManager.ApiUser;
            ApiToken = ConfigManager.ApiPassword;
        }

        private void SetupCommand()
        {
            SaveCommand = new Command(Save);
        }

        private void Save()
        {
            try
            {
                var validator = new ConfigManagerValidator();
                FluentValidation.Results.ValidationResult results = validator.Validate(ConfigManager);
                if (!results.IsValid)
                {
                    ValidationErrors = new ObservableCollection<ValidationFailure>(results.Errors);
                    return;
                }

                ConfigManager.UniqueParcles = UniqueParcles == false ? "0" : "1"; 
                ConfigManager.DeviceId = DeviceId;
                
                ConfigManager.ApiPassword = ApiToken;
                ConfigManager.ApiEndpoint = ApiEndpoint;
                ConfigManager.ApiUser = ApiUser;

                Alert("Powodzenie", "Zmiany zapisano pomyślnie", "Ok");

            }
            catch (Exception e)
            {
                Alert("Błąd krytyczny", e.Message, "Ok");
            }
        }
    }
}
