using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.App.Model
{
    public class Report
    {
        public DateTime Date { get; set; }
        public List<ScanData> Parcels { get; set; }
    }
}
