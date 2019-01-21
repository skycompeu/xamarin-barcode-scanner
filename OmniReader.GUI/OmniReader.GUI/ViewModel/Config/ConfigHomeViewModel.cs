using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using OmniReader.GUI.View.Config.Device;
using OmniReader.GUI.View.Config.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Config
{
    public class ConfigHomeViewModel : BaseViewModel
    {

        public ICommand ClearDatabaseCommand
        {
            get
            {
                return new Command(async () => await ClearDatabaseAsync());
            }
        }
        
        public ConfigHomeViewModel()
        {
            Setup();
        }

        private void Setup()
        {
            Title = "Konfiguracja";
        }

        public async Task ClearDatabaseAsync()
        {
            IsBusy = true;
            try
            {
                bool  result = await Main.DisplayAlert("Uwaga!", "Czy chcesz usunąć dane strasze niż 30 dni?", "Tak", "Nie");
                if (result) {
                    Repository<ScanData> Database = new Repository<ScanData>();
                    DateTime date = DateTime.Today.AddDays(-30);
                    List<ScanData> items = Database.Find(x => x.ScannedAt < date);

                    foreach (ScanData item in items)
                    {
                        Database.Delete(item.Id);
                    }

                    Alert("Sukces", "Operacja przebiegła pomyślnie!", "Ok.");
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
    }
}
