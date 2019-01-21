using OmniReader.Core.Generic;
using OmniReader.Core.Manager;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OmniReader.Core.View
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Xamal view Title
        /// </summary>
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Global Acces to Config
        /// </summary>
        public ConfigManager ConfigManager
        {
            get => GlobalInstance<ConfigManager>.GetInstance();
        }

        /// <summary>
        /// Global Acces to user session
        /// </summary>
        public SessionManager SessionManager
        {
            get => GlobalInstance<SessionManager>.GetInstance();
        }
        
        public BaseViewModel()
        {

        }
        
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        
        private INavigation _navigation;
        public INavigation Navigation
        {
            get => _navigation;
            set => _navigation = value;
        }

        public Page Main
        {
            get => Application.Current?.MainPage;
            set => Application.Current.MainPage = value;
        }

        public IReadOnlyList<Page> ModalStack => _navigation?.ModalStack;
        public IReadOnlyList<Page> NavigationStack => _navigation?.NavigationStack;
        
        public void InsertPageBefore(Page page, Page before)
        {
            _navigation?.InsertPageBefore(page, before);
        }

        public async Task<Page> PopAsync()
        {
            var task = _navigation?.PopAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task<Page> PopAsync(bool animated)
        {
            var task = _navigation?.PopAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }
        
        public async Task<Page> PopModalAsync()
        {
            var task = _navigation?.PopModalAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task<Page> PopModalAsync(bool animated)
        {
            var task = _navigation?.PopModalAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync()
        {
            var task = _navigation?.PopToRootAsync();
            if (task != null) await task;
        }

        public async Task PopToRootAsync(bool animated)
        {
            var task = _navigation?.PopToRootAsync(animated);
            if (task != null) await task;
        }

        public async Task PushAsync(Page page)
        {
            var task = _navigation?.PushAsync(page);
            if (task != null) await task;
        }

        public async Task PushAsync(Page page, bool animated)
        {
            var task = _navigation?.PushAsync(page, animated);
            if (task != null) await task;
        }

        public async Task PushModalAsync(Page page)
        {
            var task = _navigation?.PushModalAsync(page);
            if (task != null) await task;
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            var task = _navigation?.PushModalAsync(page, animated);
            if (task != null) await task;
        }

        public void RemovePage(Page page)
        {
            _navigation?.RemovePage(page);
        }
        
        public void Alert(string title, string message, string cancel)
        {
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public virtual void OnAppearing()
        {

        }
       
        public virtual void OnDisappearing()
        {
            
        }



        private string m_NotifyColor;
        public string NotifyColor
        {
            get => m_NotifyColor;
            set => SetProperty(ref m_NotifyColor, value);
        }

        private void SetNotifyLabelColor(string color)
        {
            if (color == "Green") NotifyColor = "Green";
            if (color == "Red") NotifyColor = "Red";
            if (color == "Yellow") NotifyColor = "Yellow";
            if (color == "Default") NotifyColor = "Default";
        }
        public void NotifySuccess()
        {
            SetNotifyLabelColor("Green");
        }
        public void NotifyError()
        {
            SetNotifyLabelColor("Red");
        }

        public void NotifyWarning()
        {
            SetNotifyLabelColor("Yellow");
        }
        public void NotifyIdle()
        {
            SetNotifyLabelColor("Green");
        }

        

        




    }
}
