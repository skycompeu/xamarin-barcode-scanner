using FluentValidation.Results;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.Data.Database.Validator;
using OmniReader.Data.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Config.User
{
    public class UserItemViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        private Repository<AppUser> Database;

        //public ObservableCollection<ValidationFailure> ValidationErrors { get; set; } = new ObservableCollection<ValidationFailure>();

        private ObservableCollection<ValidationFailure> m_ValidationErrors;
        public ObservableCollection<ValidationFailure> ValidationErrors
        {
            get => m_ValidationErrors;
            set => SetProperty(ref m_ValidationErrors, value);
        }
        
        public ICommand SaveCommand { get; set; }

        public UserItemViewModel(AppUser appUser = null)
        {
            User = (appUser == null) ? new AppUser() { UniqueId = 0, Active = false, SuperUser = false, ModifiedAt = DateTime.Now } : appUser;
            Setup();
            SetupCommands();
        }
        
        private void Setup()
        {
            Title = PrepareTitle();
            Database = new Repository<AppUser>();
            ValidationErrors = new ObservableCollection<ValidationFailure>();
        }

        private string PrepareTitle()
        {
            return (User.UniqueId == 0) ? "Dodawanie" : "Edycja";
        }

        private void SetupCommands()
        {
            SaveCommand = new Command(Save);
        }

        private void Save() {
            
            var validator = new AppUserValidator();
            ValidationResult results = validator.Validate(User);

            bool success = results.IsValid;
            if (!success)
            {
                ValidationErrors = new ObservableCollection<ValidationFailure>(results.Errors);
                return;
            }

            var users = Database.GetAll();

            try
            {
                if (User != null) User.ModifiedAt = DateTime.Now;

                if (User.UniqueId == 0) {
                    User.UniqueId = DatabaseHelper.GenerateUniqueId();
                    Database.Insert(User);
                }
                    
                if (User.UniqueId > 0) Database.Update(User);
                
                Alert("Sukces", "Zapisano pomyślnie", "Ok");
            }
            catch (Exception e)
            {
                Alert("Błąd krytyczny", e.Message, "Ok");
            }
        }
    }
}
