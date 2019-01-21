using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Report
{
    class RaportPreviewViewModel : BaseViewModel
    {

        public AppUser User { get; set; }

        public ICommand DeleteRecordCommand
        {
            get
            {
                return new Command<ScanData>(DeleteScannedData);
            }
        }

        public ICommand DeleteAllCommand
        {
            get
            {
                return new Command(async() => await DeleteAllAsync());
            }
        }

        private void DeleteScannedData(ScanData data)
        {
            try
            {
                if (User == null) return;
                if (!User.SuperUser) {

                    Alert("Uwaga", "Brak uprawnień! Zaloguj się na użytkownika posiadającego prawa administracyjne!", "Ok.");
                    return;
                }

                IsBusy = true;

                if (Delete(data))
                {
                    var items = ReportList.Where(w => w.DataValue == data.DataValue).ToList();
                    foreach (ScanData item in items)
                    {
                        ReportList.Remove(item);
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

        private async Task DeleteAllAsync()
        {
            bool result = true;
            IsBusy = true;
            try
            {
                if (User == null) return;
                if (!User.SuperUser)
                {

                    //Uwaga. Dane zostaną trwale usunięte!
                    Alert("Uwaga", "Brak uprawnień! Zaloguj się na użytkownika posiadającego prawa administracyjne!", "Ok.");
                    return;
                }

                string msg = $"Czy chesz wyczyścić dane raportu z dn.: {m_Date.ToString("yyyy-MM-dd")} ? W";

                result = await Main.DisplayAlert("Uwaga", msg, "Tak", "Nie");
                if (result) {
                    List<ScanData> data = GetScannedData();
                    if (data != null) {
                        foreach (ScanData item in data)
                        {
                            Database.Delete(item.Id);
                        }
                    }

                    Alert("Sukces", "Usuwanie zakończone!", "Ok.");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private Repository<ScanData> Database;


        public ObservableCollection<ScanData> m_ReportsList;
        public ObservableCollection<ScanData> ReportList
        {
            get => m_ReportsList;
            set => SetProperty(ref m_ReportsList, value);
        }

        private DateTime m_Date;

        public RaportPreviewViewModel(string date)
        {
            m_Date = DateTime.Parse(date).Date;
            Setup();
        }
        private void Setup()
        {


            try
            {
                IsBusy = true;

                if (SessionManager.User != null)
                {
                    User = SessionManager.User;
                }
                
                Database = new Repository<ScanData>();

                List<ScanData> data = GetScannedData();

                ReportList = new ObservableCollection<ScanData>(data);

                Title = $"{m_Date.ToString("yyyy-MM-dd")}";
                
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");
            }
            finally {
                IsBusy = false;
            }
        }

        private List<ScanData> GetScannedData() {
            DateTime from = DateTime.Parse($"{m_Date.ToString("yyyy-MM-dd")} 00:00:01");
            DateTime to = DateTime.Parse($"{m_Date.ToString("yyyy-MM-dd")} 23:59:59");
            return  Database.GetTable().Where(x => x.ScannedAt >= from && x.ScannedAt <= to).ToList();
        }


        private bool Delete(ScanData scanData)
        {
            int affected = Database.Delete(scanData.Id);
            return (affected > 0) ? true : false;
        }

    }
}
