using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using SQLite;
using System;
using System.IO;

namespace OmniReader.Data.Database
{
    public class DatabaseContext
    {
        public string DbDir { get; set; }
        public string DbName { get; set; }
        public string DbPath { get; private set; }

        public SQLite.SQLiteConnection Connection { get; private set; }

        public DatabaseContext()
        {
            if (string.IsNullOrEmpty(DbDir)) {
                DbDir = Path.Combine("./sdcard", "omni_reader", "database");
            }

            if (string.IsNullOrEmpty(DbName))
            {
                DbName = "or_database.db";
            }

            DbPath = Path.Combine(DbDir, DbName);
            //DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "or_database.db");
        }

        public void DeleteDatabase()
        {
            if (File.Exists(DbPath))
            {
                File.Delete(DbPath);
            };
        }

        public void CreateDatabase()
        {
            if (!File.Exists(DbPath))
            {
                if (!Directory.Exists(DbDir)) {
                    Directory.CreateDirectory(DbDir);
                }
                
                System.Data.SQLite.SQLiteConnection.CreateFile(DbPath);
            }
        }

        public void DebugSeed()
        {
            Repository<AppUser> UserRepo = new Repository<AppUser>();
            if(UserRepo.Find(f => f.Name == "John" && f.Surname == "Doe").Count == 0)
            {
                Random rnd = new Random();
                int UniqueId = rnd.Next(1000, 9999);

                AppUser user = new AppUser();
                user.UniqueId = UniqueId;
                user.Name = "John";
                user.Surname = "Doe";
                user.SuperUser = true;
                user.Active = true;
                user.Pin = "1111";
                user.ModifiedAt = DateTime.Now;
                UserRepo.Insert(user);
            }
        }
        
        public void DebugSeedScanData()
        {
            Repository<ScanData> Database = new Repository<ScanData>();

            Random rnd = new Random();
            int parcels = rnd.Next(1, 500); // creates a number between 1 and 12
            

            int days = DateTime.Now.Day;
            for (int i = 1; i < days; i++)
            {
                string date = String.Format("2018-12-{0:00}", i);
                Populate(date, parcels, Database);
            }

        }

        public void CreateAppAdminnistrator()
        {
            Repository<AppUser> UserRepo = new Repository<AppUser>();
            var list = UserRepo.Find(f => f.UniqueId == 1 && f.UniqueId == 1);

            if (list.Count == 0)
            {
                AppUser user = new AppUser();
                user.UniqueId = 1;
                user.Name = "App";
                user.Surname = "Aministrator";
                user.SuperUser = true;
                user.Active = true;
                user.Pin = "9610";
                user.ModifiedAt = DateTime.Now;
                UserRepo.Insert(user);
            }
        }

        Random rnd = new Random();
        private void Populate(string date, int count, Repository<ScanData> Database)
        {
            for (int i = 0; i < count; i++)
            {
                int hour = rnd.Next(1, 24);   
                int minute = rnd.Next(1, 60);   
                int second = rnd.Next(60);     

                DateTime dt = Convert.ToDateTime(date);

                dt = Convert.ToDateTime(new DateTime(dt.Year, dt.Month, dt.Day, hour, minute, second).ToString("yyyy-MM-dd h:mm:ss"));

                ScanData d = new ScanData()
                {
                    Id = 1,
                    IdScanner = 1,
                    IdUser = 1,
                    IdType = 1,
                    AdditionalServiceId = 1,
                    DataValue = RandomString(20),
                    ScannedAt = dt
                };

                Database.Insert(d);
            }
        }

        public static string RandomString(int length)
        {
            
            string str = string.Empty;
            do
            {
                str += Guid.NewGuid().ToString().Replace("-", "");
            }
            while (length > str.Length);

            return str.Substring(0, length);
        }

        public void CreateTables()
        {
            Connection = GetConnection();
            Connection.CreateTable<AppConfig>();
            Connection.CreateTable<AppUser>();

            Connection.CreateTable<ScanData>();
            Connection.CreateTable<ScanTypeV2>();
            Connection.CreateTable<AdditionalService>();
           
        }

        public SQLite.SQLiteConnection GetConnection()
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new SQLite.SQLiteConnection(DbPath);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return connection;
        }

        private string PrepareConnectionString()
        {
            return $"Data Source=x{DbPath};Version=3;";
        }
    }
}