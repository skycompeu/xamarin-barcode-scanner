using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.Database.Model
{
    [Table("or_ScanDataService")]
    public class AdditionalService
    {
        //[PrimaryKey, AutoIncrement, Indexed]
        //public int Id { get; set; }

        [PrimaryKey, Indexed]
        public int UniqueId { get; set; }
        
        [System.ComponentModel.DisplayName(@"Nazwa usługi")]
        [MaxLength(64)]
        public string Name { get; set; }

        [System.ComponentModel.DisplayName(@"Aktywna")]
        public bool Active { get; set; }

        public DateTime ModifiedAt { get; set; }

    }
}
