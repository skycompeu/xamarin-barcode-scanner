using FreshMvvm;
using OmniReader.Core.EMDK;
using OmniReader.Core.Scanner;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.GUI.View.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace OmniReader.GUI.ViewModel.Scan
{
    class ScannerViewModel : BaseViewModel
    {
        // ----------------------------------------------------------------
        public ScanType ScanType { get; set; }
        public AdditionalService AdditionalService { get; set; }
        
        // ----------------------------------------------------------------
        private Repository<ScanData> Database;
        
        // ----------------------------------------------------------------
        #region commands
        public ICommand DeleteRecordCommand
        {
            get
            {
                return new Command<ScanData>(DeleteScannedData);
            }
        }

        public ICommand InsertRecordCommand
        {
            get
            {
                return new Command(InsertRecord);
            }
        }
        
        public ICommand NavigateToReportCommand
        {
            get
            {
                return new Command(NavigateToReport);
            }
        }
        
        private async void NavigateToReport(object obj)
        {
            await Navigation.PushAsync(new ReportHomeView());
        }
        
        private void InsertRecord()
        {
            if (string.IsNullOrEmpty(ParcelNumber) == true ||ParcelNumber.Length <= 1)
            {
                Alert("Uwaga", "Numer jest niepoprawny!", "Ok");
                return;
            };

            InsertScannedData(ParcelNumber.ToUpper());
        }
        #endregion
        

        // ----------------------------------------------------------------
        private ObservableCollection<ScanData> m_InputDataList;
        public ObservableCollection<ScanData> InputDataList
        {
            get => m_InputDataList;
            set => SetProperty(ref m_InputDataList, value);
        }


        /// <summary>
        /// Hide add record
        /// </summary>
        private bool m_HidePanel;
        public bool HidePanel
        {
            get => m_HidePanel;
            set => SetProperty(ref m_HidePanel, value);
        }
        
        private string m_ParcelNumber;
        public string ParcelNumber
        {
            get => m_ParcelNumber;
            set => SetProperty(ref m_ParcelNumber, value);
        }


        private string m_ScannerStatus;
        public string ScannerStatus
        {
            get => m_ScannerStatus;
            set => SetProperty(ref m_ScannerStatus, value);
        }


        private int UserId { get; set; }
        private int ScannerId { get; set; }
        private int AdditionalServiceId { get; set; }
        
        /// <summary>
        /// Nazwa usługi dodatkowej
        /// </summary>
        public string LvServiceName { get; set; } = String.Empty;
        
        /// <summary>
        /// Typ skanowania: Pojedyncze, Grupowe, Usługa 
        /// </summary>
        public string LvScanType { get; set; } = String.Empty;






        // ----------------------------- CTOR -----------------------------
        public ScannerViewModel(ScanType scanType, AdditionalService additionalService = null)
        {
            //set from ctor params
            ScanType = scanType;
            AdditionalService = additionalService;

            Setup();
        }

        private void Setup()
        {
            HidePanel = false;
            ScannerStatus = "";

            PrepareTitle();
            
            Database = new Repository<ScanData>();
            InputDataList = new ObservableCollection<ScanData>();
            
            UserId = SessionManager.User.UniqueId;
            ScannerId = Convert.ToInt32(ConfigManager.DeviceId);
            AdditionalServiceId = PrepareAdditionalServiceId();
            
            LvScanType = PrepareScanTypeName();
            LvServiceName = PrepareServiceName();

        }

        private string PrepareServiceName()
        {
            string name = String.Empty;
            if (AdditionalService != null)
            {
                name = AdditionalService.Name;
            }
            return name;
        }

        private string PrepareScanTypeName()
        {
            string name = String.Empty;

            switch (ScanType)
            {
                case ScanType.SingleParcel:
                    name = "Pojedyncze";
                    break;

                case ScanType.SingleParcelWithService:
                    name = "Usługa";
                    break;

                case ScanType.GroupParcel:
                    name = "Grupowe";
                    break;
            }
            return name;
        }

        private int PrepareAdditionalServiceId()
        {
            return (AdditionalService != null) ? AdditionalService.UniqueId : 0;
        }

        private void PrepareTitle()
        {
            if (ScanType == ScanType.SingleParcel)
            {
                Title = "Typ: pojedyncze";
            }

            if (ScanType == ScanType.GroupParcel)
            {
                Title = "Typ: grupowe";
            }

            if (ScanType == ScanType.SingleParcelWithService)
            {
                if(AdditionalService != null)
                {
                    Title = $"Typ: {AdditionalService.Name}";
                }
            }
        }

        //--------------------------------------------------------------------------------------------
        //Scanner
        public override void OnAppearing()
        {
            try
            {
                IEMDKScanner Scanner = FreshIOC.Container.Resolve<IEMDKScanner>();
                Scanner.OnDataReceived -= Scanner_OnDataReceived;
                Scanner.OnStatusChanged -= Scanner_OnStatusChanged;
                Scanner.OnDataReceived += Scanner_OnDataReceived;
                Scanner.OnStatusChanged += Scanner_OnStatusChanged;
                Scanner.EnableScanner();
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok");
            }
        }

        public override void OnDisappearing()
        {
            try
            {
                IEMDKScanner Scanner = FreshIOC.Container.Resolve<IEMDKScanner>();
                Scanner.OnDataReceived -= Scanner_OnDataReceived;
                Scanner.OnStatusChanged -= Scanner_OnStatusChanged;
                Scanner.DisableScanner();
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok");
            }
        }
        
        //---------------------------------------------------------------------------------------------------
        private void Scanner_OnDataReceived(object sender, EMDKDataReceivedArgs e)
        {
            InsertScannedData(e.Data);
        }

        private void Scanner_OnStatusChanged(object sender, EMDKStatusChangedArgs e)
        {
            if (!string.IsNullOrEmpty(e.ScannerState))
            {
                ScannerStatus = e.ScannerState;
            }
        }
        
        //---------------------------------------------------------------------------------------------------
        private void InsertScannedData(string scanData)
        {
            IsBusy = true;
            bool debugMode = false;

            ScanData @new = PrepareScanData(scanData);
            try
            {
                if (!IsUnique(@new, debugMode))
                {
                    BeepWarning();
                    return;
                }
                
                if (Insert(@new))
                {
                    InputDataList.Add(@new);
                }
            }
            catch (Exception ex)
            {
                BeepError();
                Alert("Błąd!", ex.Message, "Ok.");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void DeleteScannedData(ScanData scanData)
        {
            try
            {
                IsBusy = true;

                if (Delete(scanData))
                {
                    var items = InputDataList.Where(w => w.DataValue == scanData.DataValue).ToList();
                    foreach (ScanData item in items)
                    {
                        InputDataList.Remove(item);
                    }

                    Alert("Sukcess", "Usuwanie zakończone.", "Ok.");
                }
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");
            }
            finally {
                IsBusy = false;
            }
        }
        
        //------------------------------------------------------------------------------------------
        private void BeepWarning() {
            IEMDKScanner Scanner = FreshIOC.Container.Resolve<IEMDKScanner>();
            if (Scanner != null)
            {
                Scanner.NotifyWarning();
            }
        }

        private void BeepError() {
            IEMDKScanner Scanner = FreshIOC.Container.Resolve<IEMDKScanner>();
            if (Scanner != null) {
                Scanner.NotifyError();
            }
        }
        
        //------------------------------------------------------------------------------------------
        private bool IsUnique(ScanData sc, bool debugMode)
        {
            return (Database.Find(f => f.DataValue == sc.DataValue).Count == 0 && !debugMode);
        }
        
        private bool Insert(ScanData scanData)
        {
            return (Database.Insert(scanData) > 0) ? true : false;
        }

        private bool Delete(ScanData scanData)
        {
            int affected = Database.Delete(scanData.Id);
            return (affected > 0) ? true : false ;
        }
        
        private ScanData PrepareScanData(string data)
        {
            return new ScanData
            {
                IdUser = UserId,
                IdScanner = ScannerId,
                DataValue = data.Trim(),
                ScannedAt = DateTime.Now,
                AdditionalServiceId = AdditionalServiceId,
                IdType = Convert.ToInt32(ScanType)
            };
        }
    }
}
