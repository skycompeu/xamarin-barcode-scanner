using OmniReader.Core.Generic;
using OmniReader.Core.Manager;
using OmniReader.Data.Database;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace OmniReader.Core.App
{
    public class AppInitializer
    {
        public void InitializeConfig()
        {
            ConfigManager cm = GlobalInstance<ConfigManager>.GetInstance();
            
            if (string.IsNullOrEmpty(cm.ApiEndpoint))
            {
                cm.ApiEndpoint = "https://example.com/api/v1/omnireader";
            }

            if (string.IsNullOrEmpty(cm.ApiUser))
            {
                cm.ApiUser = "user";
            }

            if (string.IsNullOrEmpty(cm.ApiPassword))
            {
                cm.ApiPassword = "hidden-password";
            }

            if (string.IsNullOrEmpty(cm.DeviceId))
            {
                cm.DeviceId = "1";
            }

            if (string.IsNullOrEmpty(cm.UniqueParcles))
            {
                cm.UniqueParcles = "1";
            }
        }
        
        public void InitalizeDatabase()
        {
            DatabaseContext d = new DatabaseContext();
            //d.DeleteDatabase();
            d.CreateDatabase();
            d.CreateTables();
            d.CreateAppAdminnistrator();
        }

        public void InitalizeGlobalObjects()
        {
            GlobalInstance<SessionManager>.GetInstance();
            GlobalInstance<ConfigManager>.GetInstance();
        }
    }
}
