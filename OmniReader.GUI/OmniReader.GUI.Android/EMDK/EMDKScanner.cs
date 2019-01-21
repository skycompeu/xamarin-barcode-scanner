using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OmniReader.Core.EMDK;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using Symbol.XamarinEMDK.Notification;
using static Symbol.XamarinEMDK.Barcode.ScannerConfig;

namespace OmniReader.GUI.Droid.Emdk
{
    public class EMDKScanner: Java.Lang.Object, EMDKManager.IEMDKListener, IEMDKScanner
    {
        //--------------------------------------------------------------------------
        private Context context;

        //--------------------------------------------------------------------------
        public event EventHandler<EMDKDataReceivedArgs> OnDataReceived;
        public event EventHandler<EMDKStatusChangedArgs> OnStatusChanged;
        
        //--------------------------------------------------------------------------
        EMDKManager m_EmdkManager = null;
        BarcodeManager m_BarcodeManager = null;
        //Symbol.XamarinEMDK.Notification.NotificationManager m_NotificationManager = null;
        //NotificationDevice m_NotificationDevice = null;
        Scanner m_Scanner = null;

        //--------------------------------------------------------------------------
        private bool ConfigChange = true;
        
        #region ctors
        public EMDKScanner(Context context)
        {
            this.context = context;
        }
        #endregion

        #region API Calls
        public void StartScanner()
        {
            getEMDKManager();
        }

        public void ResumeScanner()
        {
            InitScanner();
        }

        public void PauseScanner()
        {
            DeInitScanner();
        }

        public void DestroyScanner()
        {
            DestroyScannerManager();
        }

        public void DisableScanner()
        {
            m_Scanner.Disable();
        }

        public void EnableScanner()
        {
            m_Scanner.Enable();
        }

        #endregion

        private void getEMDKManager()
        {
            string strResults = string.Empty;
            EMDKResults results = EMDKManager.GetEMDKManager(Application.Context, this);
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                strResults = "EMDKManager object creation failed!";
            }
            else
            {
                strResults = "EMDKManager object creation succeeded.";
            }

            OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs(strResults));
        }

        void EMDKManager.IEMDKListener.OnClosed()
        {
            OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs(" EMDK Open failed unexpectedly."));
            if (m_EmdkManager != null)
            {
                m_EmdkManager.Release();
                m_EmdkManager = null;
            }
            ConfigChange = true;
        }

        void EMDKManager.IEMDKListener.OnOpened(EMDKManager p0)
        {
            OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs("EMDK Opened successfully."));
            m_EmdkManager = p0;

            InitScanner();
        }


        void InitScanner()
        {
            if (m_EmdkManager != null)
            {
                if (m_BarcodeManager == null)
                {
                    try
                    {
                        m_BarcodeManager = (BarcodeManager)m_EmdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
                        m_Scanner = m_BarcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);
                        
                        if (m_Scanner != null)
                        {
                            m_Scanner.Data -= DataReceived;
                            m_Scanner.Data += DataReceived;
                            m_Scanner.Status -= StatusChanged;
                            m_Scanner.Status += StatusChanged;
                        }
                        else
                        {
                            OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs("Failed to enable scanner."));
                        }
                    }
                    catch (ScannerException e)
                    {
                        OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"Error: {e.Message}"));
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"Error: {ex.Message}"));
                    }
                }
            }
        }

        private void DeInitScanner()
        {
            if (m_EmdkManager != null)
            {
                if (m_Scanner != null)
                {
                    try
                    {
                        m_Scanner.Data -= DataReceived;
                        m_Scanner.Status -= StatusChanged;
                        m_Scanner.Disable();
                    }
                    catch (ScannerException e)
                    {
                        OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"Error: {e.Message}"));
                    }
                }

                if (m_BarcodeManager != null)
                {
                    m_EmdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }

                m_BarcodeManager = null;
                m_Scanner = null;
            }
        }

        private void DataReceived(object sender, Scanner.DataEventArgs e)
        {
            OnDataReceived?.Invoke(this, new EMDKDataReceivedArgs(PrepareReceivedData(e.P0), ""));
        }
        
        private string PrepareReceivedData(ScanDataCollection p0)
        {
            string prepared = String.Empty;
            ScanDataCollection scanDataCollection = p0;
            if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();
                foreach (ScanDataCollection.ScanData data in scanData)
                {
                    prepared = data.Data.Trim();
                }
            }
            return prepared;
        }
        
        private void StatusChanged(object sender, Scanner.StatusEventArgs e)
        {
            string status = String.Empty;
            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"Gotowy."));
                
                try
                {
                    if (m_Scanner.IsEnabled && !m_Scanner.IsReadPending)
                    {
                        if (ConfigChange)
                        {
                            SetConfig();
                            ConfigChange = false;
                        }

                        m_Scanner.Read();
                    }
                }
                catch (ScannerException ex)
                {
                    status = ex.Message;
                    OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{status}"));
                }
            }

            if (state == StatusData.ScannerStates.Waiting)
            {
                status = "Oczekiwanie..";
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{status}"));
            }
            if (state == StatusData.ScannerStates.Scanning)
            {
                status = "Skanowanie...";
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{status}"));
            }
            if (state == StatusData.ScannerStates.Disabled)
            {
                status = "Scanner disabled";
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{status}"));
            }
            if (state == StatusData.ScannerStates.Error)
            {
                status = "Error occurred during scanning";
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{status}"));
            }
        }

        private void SetConfig()
        {
            try
            {
                ScannerConfig config = m_Scanner.GetConfig();
                config.ScanParams.DecodeLEDFeedback = true;
                
                config.ScanParams.DecodeLEDFeedback = true;
                config.ScanParams.AudioStreamType = AudioStreamType.Ringer;

                //scan types
                config.DecoderParams.Code39.Enabled = true;
                config.DecoderParams.Code128.Enabled = true;
                config.DecoderParams.MaxiCode.Enabled = true;
                config.DecoderParams.MicroPDF.Enabled = true;
                config.DecoderParams.Pdf417.Enabled = true;
                config.DecoderParams.Aztec.Enabled = true;
                config.DecoderParams.Upca.Enabled = true;
                config.DecoderParams.Ean13.Enabled = true;
                config.DecoderParams.Ean8.Enabled = true;
                config.DecoderParams.Code39.Enabled = true;
                config.DecoderParams.Code128.Enabled = false;
                config.DecoderParams.Ean8.Enabled = true;
                config.DecoderParams.Ean13.Enabled = true;
                config.DecoderParams.Code11.Enabled = true;
                config.DecoderParams.Code39.Enabled = true;
                config.DecoderParams.Code93.Enabled = true;
                config.DecoderParams.Code128.Enabled = true;
                config.DecoderParams.Upca.Enabled = true;
                config.DecoderParams.Upce0.Enabled = true;
                config.DecoderParams.Upce1.Enabled = true;
                config.DecoderParams.Upce1.Enabled = true;
                config.DecoderParams.D2of5.Enabled = true;
                config.DecoderParams.I2of5.Enabled = true;
                config.DecoderParams.Pdf417.Enabled = true;
                config.DecoderParams.QrCode.Enabled = true;
                
                m_Scanner.SetConfig(config);
            }
            catch (ScannerException e)
            {
                OnStatusChanged?.Invoke(this, new EMDKStatusChangedArgs($"{e.Message}"));
            }
        }

        private void DestroyScannerManager()
        {
            if (m_EmdkManager != null)
            {
                m_EmdkManager.Release();
                m_EmdkManager = null;
            }
        }
    
        public void NotifyError()
        {
            ToneGenerator generator = new ToneGenerator(Android.Media.Stream.Alarm, 100);
            generator.StartTone(Tone.CdmaAbbrAlert);
            SystemClock.Sleep(600);
            generator.Release();
        }

        public void NotifyWarning()
        {
            ToneGenerator generator = new ToneGenerator(Android.Media.Stream.Alarm, 100);
            generator.StartTone(Tone.CdmaCalldropLite);
            SystemClock.Sleep(600);
            generator.Release();
        }
    }
}