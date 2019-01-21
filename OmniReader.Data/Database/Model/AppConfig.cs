using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.Database.Model
{
    [Table("or_AppConfig")]
    public class AppConfig
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string Value { get; set; }
    }
}
