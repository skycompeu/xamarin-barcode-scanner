using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OmniReader.Data.Database.Repository;
using SQLite;

namespace OmniReader.Data.Database.Model
{
    [Table("or_User")]
    public class AppUser
    {
        //[PrimaryKey, AutoIncrement, Indexed]
        //public int Id { get; set; }


        [PrimaryKey, Indexed]
        public int UniqueId { get; set; }
        
        [System.ComponentModel.DisplayName(@"Imię")]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [System.ComponentModel.DisplayName(@"Nazwisko")]
        [MaxLength(255)]
        public string Surname { get; set; }


        [Ignore]
        public string FullName {
            get
            {
                return $"{Name} {Surname}";
            }
        }


        [MaxLength(4)]
        public string Pin { get; set; }
        public bool SuperUser { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}