using OmniReader.Core.API;
using OmniReader.Core.Extensions;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Report
{
    class ReportSyncViewModel : BaseViewModel
    {
        public Data.App.Model.Report  Report { get; set; }
        private OmniClient m_OmniClient;



        


        private double m_Progress;
        public double ProgressBarValue
        {
            get => m_Progress;
            set => SetProperty(ref m_Progress, value);
        }


        public string Info { get; set; }

        

        



        private bool m_Stop = false;


        private string m_ReportInfo;
        public string ReportInfo
        {
            get => m_ReportInfo;
            set => SetProperty(ref m_ReportInfo, value);
        }

        private string m_ReportPeriod;
        public string ReportPeriod
        {
            get => m_ReportPeriod;
            set => SetProperty(ref m_ReportPeriod, value);
        }



        #region BTN
        private bool m_BtnStartIsEnabled;
        public bool BtnStartEnabled
        {
            get => m_BtnStartIsEnabled;
            set => SetProperty(ref m_BtnStartIsEnabled, value);
        }
        
        private bool m_BtnStopIsEnabled;
        public bool BtnStopEnabled
        {
            get => m_BtnStopIsEnabled;
            set => SetProperty(ref m_BtnStopIsEnabled, value);
        }
        #endregion

        public ICommand StartSyncCommand { get; set; }
        public ICommand StopSyncCommand { get; set; }


        public ReportSyncViewModel(Data.App.Model.Report report)
        {
            Report = report;
            
            Setup();
            SetupCommands();
        }

        private void Setup()
        {
            GuiPrepare();
            m_OmniClient = new OmniClient(ConfigManager.ApiUser, ConfigManager.ApiPassword, ConfigManager.ApiEndpoint);
        }

        private void GuiPrepare()
        {
            Title = "Synchronizacja";
            ReportPeriod = Report.Date.ToString("yyyy-MM-dd");
            BtnStartEnabled = true;
            BtnStopEnabled = false;
            ReportInfo = "--/--";
            ProgressBarValue = 0.0;
        }

        private void SetupCommands()
        {
            StartSyncCommand = new Command(ParcelPush);
            StopSyncCommand = new Command(StopSync);
        }
        
        private async void  ParcelPush() {
            
            GuiStatus(false);
            m_Stop = false;

            ReportInfo = "Rozpoczęcie synchronizacji...";

            List<List<ScanData>> chunks = Report.Parcels.ChunkBy(10);
            int currentChunk = 1;
            int chunkCount = chunks.Count;

            try
            {
                ReportInfo = "Aktualizacja API...";
                foreach (var parcelChunk in chunks)
                {
                    int fail = 0;
                    await Task.Run(() =>
                    {
                        bool ok = false;
                        do
                        {
                            try
                            {
                               
                                List<ScanData> pc = parcelChunk.Where(w => w.DataValue != null && w.DataValue != "").ToList();
                                ok = m_OmniClient.PushParcels(pc);
                            }
                            catch (Exception ex)
                            {
                                //shit code but no time
                                //TO-DO
                                if (fail == 3) {
                                    if (!string.IsNullOrEmpty(ex.Message))
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                    else {
                                        break;
                                    }
                                }
                                fail++;
                            }

                            ReportInfo = PrapareInfo(currentChunk, chunkCount, fail);

                        } while (ok == false);
                    });

                    ReportInfo = PrapareInfo(currentChunk, chunkCount, fail);
                    ProgressBarValue = PrepareProgressValue(currentChunk, chunkCount);
                    currentChunk++;

                    if (m_Stop == true)
                    {
                        ReportInfo = "Zatrzymano";
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                ReportInfo = "Synchronizacja. Błąd";

                if (e.Message == "Error: NameResolutionFailure")
                {
                    Alert("Błąd krytyczny", "Brak połączenia z internetem!", "Ok");
                }
                else
                {
                    Alert("Błąd krytyczny", $"Błąd API: {e.Message}", "Ok");
                }
            }
            finally {
                m_Stop = false;
                GuiStatus(true);
            }    
        }

        private string PrapareInfo(int currentChunk, int chunkCount, int fail)
        {
            return $"Wysyłanie: {currentChunk}/{chunkCount} | P: {fail} | {PrepareProgressValue(currentChunk, chunkCount, 1).ToString("0.##")}%";
        }

        private double PrepareProgressValue(int currentChunk, int chunkCount, double valueToProgressFactor = 0.01)
        {
            return (Convert.ToDouble(currentChunk) / Convert.ToDouble(chunkCount) * 100) * valueToProgressFactor;
        }

        private void StopSync(object obj)
        {
            m_Stop = true;
        }

        private void GuiStatus(bool status) {
            Device.BeginInvokeOnMainThread(() => {
                BtnStartEnabled = status;
                BtnStopEnabled = !status;
                IsBusy = !status;
            });
        }
    }
}
