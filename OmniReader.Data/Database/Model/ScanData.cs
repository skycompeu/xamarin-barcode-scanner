using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace OmniReader.Data.Database.Model
{
    [Table("or_ScanData")]
    public class ScanData
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdScanner { get; set; }
        public int IdType { get; set; }                // typ skanowania ScanType enum.
        public int AdditionalServiceId { get; set; }   //typ usługi pakowania. jesli wybrany to jest > 0;
        
        [Indexed, MaxLength(255)]
        public string DataValue { get; set; }

        [Indexed]
        public DateTime ScannedAt { get; set; }

    }
}