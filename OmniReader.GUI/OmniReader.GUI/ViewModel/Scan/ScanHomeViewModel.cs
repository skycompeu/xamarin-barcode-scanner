using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OmniReader.GUI.ViewModel.Scan
{
    public class ScanHomeViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; } = "Typ skanowania";


        #region
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
