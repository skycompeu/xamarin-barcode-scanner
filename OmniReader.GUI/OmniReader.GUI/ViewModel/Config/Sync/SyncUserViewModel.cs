using OmniReader.Core.API;
using OmniReader.Core.EMDK;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Config.User
{
    public class SyncUserViewModel : BaseViewModel
    {
        private Repository<AppUser> Database;
        private OmniClient m_OmniClient;
        private IAudioNotify m_AudioNotify;
        
        public ICommand StartSync { get; set; }

        private string m_Info;
        public string Info
        {
            get => m_Info;
            set => SetProperty(ref m_Info, value);
        }
        
        private string m_SyncType;
        public string SyncType
        {
            get => m_SyncType;
            set => SetProperty(ref m_SyncType, value);
        }

        private bool m_BtnStartEnabled;
        public bool BtnStartEnabled
        {
            get => m_BtnStartEnabled;
            set => SetProperty(ref m_BtnStartEnabled, value);
        }
        
        public SyncUserViewModel()
        {
            Setup();
            SetupCommands();
        }

        #region commands
        private void SetupCommands()
        {
            StartSync = new Command(StartSyncCommand);
        }
        #endregion

        private void Setup()
        {
            Title = "Synchronizacja";
            SyncType = "Typ: użytkownicy";
            BtnStartEnabled = true;
            Info = "----";

            Database = new Repository<AppUser>();
            m_OmniClient = new OmniClient(ConfigManager.ApiUser, ConfigManager.ApiPassword, ConfigManager.ApiEndpoint);
            m_AudioNotify = DependencyService.Get<IAudioNotify>();
        }

        private void GUI(bool status)
        {
            BtnStartEnabled = status;
            IsBusy = !status;
        }

        private async void StartSyncCommand()
        {
            GUI(false);
            try
            {
                Info = "Rozpoczęcie synchronizacji...";
                List<AppUser> deviceUsers = Database.Find(w => w.UniqueId > 1 && w.UniqueId > 1 && w.Name != "App");

                await Task.Run(() => {

                    Info = "Aktualizacja API...";
                    List<AppUser> newUsers = m_OmniClient.SyncUsersV2(deviceUsers);
                    Info = "Aktualizacja API. Zakończono.";

                    Thread.Sleep(100 * 10);

                    if (newUsers != null && newUsers.Count > 0)
                    {
                        Info = "Usuwanie kont użytkowników...";
                        foreach (AppUser du in deviceUsers)
                        {
                            Database.Delete(du.UniqueId);
                        }
                        Info = "Usuwanie kont użytkowników. Zakończono.";

                        Thread.Sleep(100 * 10);

                        Info = "Dodawanie kont użytkowników...";
                        AppUser u = null;
                        try
                        {
                            foreach (AppUser nu in newUsers)
                            {
                                if (nu.Name == "App") continue;
                                u = nu;
                                Database.Insert(nu);
                            }

                            Info = "Dodawanie kont użytkowników... Zakończono.";
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"Błąd {e.Message} : {u.FullName}");
                        }
                    }
                });

                Thread.Sleep(100 * 5);

                Info = "Zakończenie synchronizacji...";
                m_AudioNotify.Ok();

                Thread.Sleep(100 * 10);

                await Navigation.PopAsync();

            }
            catch (Exception e)
            {
                Info = "Synchronizacja. Błąd";
                
                if (e.Message == "Error: NameResolutionFailure")
                {
                    Alert("Błąd krytyczny", "Brak połączenia z internetem!", "Ok");
                }
                else {
                    Alert("Błąd krytyczny", $"Nieznany błąd: {e.Message}", "Ok");
                }
            }
            finally {
                GUI(true);
            }                   
        }
    }
}
