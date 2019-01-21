using OmniReader.Core.Generic;
using OmniReader.Core.Manager;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        private string TmpPin;

        private Repository<AppUser> Database;

        public ICommand ButtonPress { private set; get; }
        
        private string m_HashPin;
        public string HashPin
        {
            get => m_HashPin;
            set => SetProperty(ref m_HashPin, value);
        }


        private bool m_CanTap = true;
        public bool CanTap
        {
            get => m_CanTap;
            set => SetProperty(ref m_CanTap, value);
        }


        private AppUser m_SelectedUser;
        public AppUser SelectedUser
        {
            get {
                CanTap = false;

                //  IsEnabled="{Binding CanTap, Converter={Helpers:InverseBoolConverter}}"

                return m_SelectedUser;
            }

            set {
                CanTap = true;
                SetProperty(ref m_SelectedUser, value);
            }
        }
        

        private ObservableCollection<AppUser> m_UserList;
        public ObservableCollection<AppUser> UserList
        {
            get => m_UserList;
            set => SetProperty(ref m_UserList, value);
        }
        
        public LoginViewModel()
        {
            CanTap = true;
            Setup();
            SetupCommands();
        }

        private void Setup()
        {
            Title = "Logownaie";
            UserName = "";

            Database = new Repository<AppUser>();
            UserList = new ObservableCollection<AppUser>(Database.GetAll());

            if (SessionManager != null) {
                if (SessionManager.User != null)
                {
                    SessionManager.Logout();
                }
            }
        }

        private void SetupCommands()
        {
            ButtonPress = new Command<Button>(ButtonPressCommand);
        }
        
        /// <summary>
        /// bad code, to refactor 
        /// </summary>
        /// <param name="btn"></param>
        private void ButtonPressCommand(Button btn)
        {
            if (SelectedUser == null)
            {
                //Alert("Uwaga", "Wybierz użytkownika!", "Ok");
                HashPin = "Wybierz użytkownika!";

                return;
            }

            string input = btn.Text;
            if (input.ToLower() == "c")
            {
                if (HashPin != null && HashPin.Length > 0) {
                    if (TmpPin != null) {
                        TmpPin = (TmpPin.Length == 0) ? "" : TmpPin.Remove(TmpPin.Length - 1);
                    }
                    HashPin = (HashPin.Length == 0) ? "" : HashPin.Remove(HashPin.Length - 1);
                    if (HashPin != null && HashPin.Length > 4) HashPin = "";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(HashPin) && HashPin.Length > 4) HashPin = "";
                TmpPin += input;
                HashPin += "*";
            }
            
            if (TmpPin !=null && TmpPin.Length == 4)
            {
                LoginMessageEnum lme = LoginMessageEnum.NotExists;
                if (SessionManager.LoginV2(TmpPin, SelectedUser.Name, SelectedUser.Surname, ref lme)) {
                    Title = "Logowanie";
                    HashPin = String.Empty;
                    Navigation.PushAsync(new HomeView(), false);
                }
                else
                {
                    switch (lme)
                    {
                        case LoginMessageEnum.BadPIN:
                            HashPin = "Błędny pin!";
                            break;
                        case LoginMessageEnum.Inactive:
                            HashPin = "Nieaktywny";
                            break;
                        default:
                            HashPin = "Błędny pin!";
                            break;
                    }
                }

                TmpPin = null;
            }
        }
    }
}
