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

namespace OmniReader.GUI.ViewModel.Config.Sync
{
    public class SyncServiceViewModel : BaseViewModel
    {

        private Repository<AdditionalService> Database;
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


        public SyncServiceViewModel()
        {
            Setup();
            SetupCommands();
        }

        private void Setup()
        {
            Title = "Synchronizacja";
            SyncType = "Typ: użytkownicy";
            BtnStartEnabled = true;
            Info = "----";

            Database = new Repository<AdditionalService>();
            m_OmniClient = new OmniClient(ConfigManager.ApiUser, ConfigManager.ApiPassword, ConfigManager.ApiEndpoint);
            m_AudioNotify = DependencyService.Get<IAudioNotify>();
        }

        private void SetupCommands()
        {
            StartSync = new Command(StartSyncCommand);
        }

        private void GUI(bool status)
        {
            BtnStartEnabled = status;
            Info = "----";
            IsBusy = !status;
        }

        private async void StartSyncCommand()
        {
            GUI(false);
            try
            {
                Info = "Rozpoczęcie synchronizacji...";
                List<AdditionalService> deviceServices = Database.GetAll();
               
                await Task.Run(() => {

                    Info = "Aktualizacja API...";
                    List<AdditionalService> newServices = m_OmniClient.SyncServicesV2(deviceServices);
                    
                    Info = "Aktualizacja API. Zakończono.";


                    if (newServices != null && newServices.Count > 0)
                    {
                        Thread.Sleep(100 * 10);
                        Info = "Usuwanie usług...";

                        foreach (AdditionalService deviceService in deviceServices)
                        {
                            Database.Delete(deviceService.UniqueId);
                        }

                        Info = "Usuwanie usług. Zakończono.";

                        Thread.Sleep(100 * 10);


                        Info = "Dodawanie usług...";
                        AdditionalService @as = null;
                        try
                        {
                            foreach (AdditionalService newService in newServices)
                            {
                                @as = newService;
                                Database.Insert(newService);
                            }

                            Info = "Dodawanie usług. Zakończono.";
                        }
                        catch (Exception e)
                        {
                            throw new Exception($"Błąd {e.Message} : {@as.Name}");
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

                m_AudioNotify.Error();

                if (e.Message == "Error: NameResolutionFailure")
                {
                    Alert("Błąd krytyczny", "Brak połączenia z internetem!", "Ok");
                }
                else
                {
                    Alert("Błąd krytyczny", $"Nieznany błąd: {e.Message}", "Ok");
                }
            }
            finally
            {
                GUI(true);
            }
        }

    }
}
