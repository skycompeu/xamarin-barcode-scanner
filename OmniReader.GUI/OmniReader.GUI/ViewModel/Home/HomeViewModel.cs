using OmniReader.Core.EMDK;
using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.GUI.View.Report;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public string Version { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public AppUser User { get; set; }
        
        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () => await LogoutAsync());
            }
        }
        
        public HomeViewModel()
        {
            Setup();
        }

        private void Setup()
        {
            try
            {
                Title = "OmniReader";
                UserName = PrepareHeaderLabel();
                Version = PrepareAppVersionV2();

                if (SessionManager.User != null)
                {
                    User = SessionManager.User;
                }
            }
            catch (System.Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");
            }
        }


        private async Task LogoutAsync()
        {
            try
            {
                bool result = await Main.DisplayAlert("Uwaga", "Czy przepowadziłeś synchronizacje?", "Tak", "Nie");
                if (result)
                {
                    Logout();
                    await Navigation.PopAsync();
                }
                else {
                    await Navigation.PushAsync(new ReportHomeView(), false);
                }
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");
            }
        }
        
        public void Logout()
        {
            if (SessionManager != null) {
                SessionManager.Logout();
            }
        }

        private string PrepareHeaderLabel()
        {
            string label = string.Empty;
            if (SessionManager.User != null) {
                label = $"{SessionManager.User.Name} {SessionManager.User.Surname}";
            }
            
            return label;
        }

        private string PrepareAppVersionV2()
        {
            return $"Wersja: {DependencyService.Get<IAppVersion>().GetVersion()}";
        }
    }
}
