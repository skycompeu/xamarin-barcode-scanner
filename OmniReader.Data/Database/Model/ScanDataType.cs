using System;
using SQLite;

namespace OmniReader.Data.Database.Model
{
    [Table("or_ScanDataType")]
    public class ScanTypeV2
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}
