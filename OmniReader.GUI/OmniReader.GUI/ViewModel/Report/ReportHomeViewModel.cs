using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OmniReader.GUI.ViewModel.Report
{
    public class ReportHomeViewModel : BaseViewModel
    {
        private Repository<ScanData> Database;

        private ObservableCollection<Data.App.Model.Report> m_ReportList;
        public ObservableCollection<Data.App.Model.Report> ReportList
        {
            get => m_ReportList;
            set => SetProperty(ref m_ReportList, value);
        }
        
        public ReportHomeViewModel()
        {
            Setup();
        }

        private void Setup()
        {
            IsBusy = true;
            try
            {
                Title = "Lista raportów";
                Database = new Repository<ScanData>();
                PrepareReports();
            }
            catch (Exception ex)
            {
                Alert("Błąd", ex.Message, "Ok.");    
            }

            IsBusy = false;
        }
        
        private void PrepareReports()
        {
            var grouped = Database.GetTable().GroupBy(p => p.ScannedAt.ToString("yyyy-MM-dd"), (key, g) => new { Day = key, ScanData = g.ToList() });
            List<Data.App.Model.Report> reports = new List<Data.App.Model.Report>();
            foreach (var g in grouped)
            {
                reports.Add(new Data.App.Model.Report()
                {
                    Date = Convert.ToDateTime(g.Day),
                    Parcels = g.ScanData,
                });
            }

            ReportList = new ObservableCollection<Data.App.Model.Report>(reports.OrderByDescending(x => x.Date));
        }
    }
}
