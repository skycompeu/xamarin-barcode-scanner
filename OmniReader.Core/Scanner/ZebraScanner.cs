using FluentValidation.Results;
using OmniReader.Core.EMDK;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.Data.Database.Validator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace OmniReader.Core.Scanner
{
    public class ZebraScanner<T> where T : class
    {
        public event EventHandler<OnInsertArg> OnInsert;
        public event EventHandler<OnErrorArg> OnError;
        public event EventHandler<OnUniqueArg> OnUnique;
        

        public ObservableCollection<ScanData> Data { get; set; } = new ObservableCollection<ScanData>();
        private T m_AppContext;
        public int UserId { get; set; }
        public int ScannerId { get; set; }
        public int ServiceId { get; set; }
        public ScanType ScanType { get; set; }
        private Repository<ScanData> Database;



        private  string SUBSCRIBE_MESSAGE = "ScanedData";


        private bool m_Disposed;


        //public ZebraScanner(T appContext)
        //{
        //    m_AppContext = appContext ?? throw new ArgumentNullException("Param: appContext can't be null!");
        //    SubscribeScanner();
        //}


        public ZebraScanner(T appContext, ScanType scanType, int appUser = 1, int scannerId = 1, int serviceId = 1)
        {
            m_AppContext = appContext ?? throw new ArgumentNullException("Param: appContext can't be null!");
            Database = new Repository<ScanData>();
            UserId = appUser;
            ScanType = scanType;
            ScannerId = scannerId;
            ServiceId = serviceId;

            SubscribeScanner();
        }


      


        private void SubscribeScanner()
        {
            MessagingCenter.Subscribe<T, string>(m_AppContext, SUBSCRIBE_MESSAGE, (sender, args) =>
            {
                Insert(args);
            });
        }


        private void UnsubscribeScanner() {
            MessagingCenter.Unsubscribe<T, string>(m_AppContext, SUBSCRIBE_MESSAGE);
        }


        private void Insert(string data)
        {
            try
            {
                ScanData @new = new ScanData
                {
                    IdScanner = ScannerId,
                    IdUser = UserId,
                    IdType = (int)ScanType,
                    AdditionalServiceId = ServiceId,
                    DataValue = data,
                    ScannedAt = DateTime.Now
                };
            
                if (!IsUnique(@new))
                {
                    OnUnique(this, new OnUniqueArg(@new)); //ponowny skan
                    return;
                }

                int affected = Database.Insert(@new);
                if (affected > 0)
                {
                    OnInsert(this, new OnInsertArg(@new));
                }

            }
            catch (Exception e)
            {
                OnError(this, new OnErrorArg(e.Message));
            }
        }

        public bool DeleteData(ScanData scanDataItem)
        {
            return (Database.Delete(scanDataItem.Id) > 0) ? true : false;
        }

        private bool IsUnique(ScanData sc)
        {
            return (Database.Find(f => f.DataValue == sc.DataValue).Count == 0) ? true : false;
        }

        public void InsertRecord(string parcel)
        {
            Insert(parcel);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                m_Disposed = true;
                UnsubscribeScanner();
            }
        }
    }
}
