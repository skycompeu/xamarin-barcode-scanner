using OmniReader.Core.Generic;
using OmniReader.Data.Database.Repository;
using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace OmniReader.Core.Manager
{
    public class ConfigManager
    {
        private static ISettings AppSettings => CrossSettings.Current;


        [System.ComponentModel.DisplayName("API: Hasło")]
        public string ApiPassword
        {
            get => AppSettings.GetValueOrDefault("api_token", string.Empty);
            set => AppSettings.AddOrUpdateValue("api_token", value);
        }

        [System.ComponentModel.DisplayName("API: Url")]
        public string ApiEndpoint
        {
            get => AppSettings.GetValueOrDefault("api_endpoint", string.Empty);
            set => AppSettings.AddOrUpdateValue("api_endpoint", value);
        }

        [System.ComponentModel.DisplayName("API: Użytkownik")]
        public string ApiUser
        {
            get => AppSettings.GetValueOrDefault("api_user", string.Empty);
            set => AppSettings.AddOrUpdateValue("api_user", value);
        }

        public string UniqueParcles
        {
            get => AppSettings.GetValueOrDefault("app_qunique_parcels", string.Empty);
            set => AppSettings.AddOrUpdateValue("app_qunique_parcels", value);
        }

        public string DeviceId
        {
            get => AppSettings.GetValueOrDefault("app_device_id", string.Empty);
            set => AppSettings.AddOrUpdateValue("app_device_id", value);
        }

        
        //private Repository<AppConfig> m_AppConfigRepo;
        //private List<AppConfig> ConfigList = new List<AppConfig>();

        //public ConfigManager()
        //{
        //    m_AppConfigRepo = new Repository<AppConfig>();
        //}

        //public void Refresh()
        //{
        //    ConfigList = m_AppConfigRepo.GetAll();
        //}

        //private bool IsPropertyExists(string properyName)
        //{
        //    bool propertyExists = false;
        //    var ls = m_AppConfigRepo.Find(f => f.Name.ToLower() == properyName.ToLower());
        //    if(ls != null && ls.Count > 0 ) propertyExists =  true;
        //    return propertyExists;
        //}

        //private int CreatePropertyIfNotExists(string name) {
        //    int Id = 0;
        //    if (IsPropertyExists(name))
        //    {
        //        Id = m_AppConfigRepo.Find(f => f.Name.ToLower() == name.ToLower())[0].Id;
        //    }
        //    else {
        //        AppConfig ac = new AppConfig();
        //        ac.Name = name;
        //        ac.Value = "";
        //        int affected = m_AppConfigRepo.Insert(ac);
        //        if(affected > 0) Id = m_AppConfigRepo.Find(f => f.Name.ToLower() == name.ToLower())[0].Id;
        //    }

        //    return Id;
        //}

        //private void SetProperyValue(string properyName, string properyValue)
        //{
        //    int propId = CreatePropertyIfNotExists(properyName);
        //    if (propId == 0) throw new Exception("AppConfig.Id cannot have 0 value!");

        //    AppConfig ac = new AppConfig();
        //    ac.Id = propId;
        //    ac.Name = properyName;
        //    ac.Value = properyValue;

        //    m_AppConfigRepo.Update(ac);
        //}


        //private string GetProperyValue(string name)
        //{
        //    List<AppConfig> lst = m_AppConfigRepo.Find(f => f.Name.ToLower() == name.ToLower());
        //    if (lst == null || lst.Count == 0) {
        //        CreatePropertyIfNotExists(name);
        //        return "";
        //    };
        //    return lst[0].Value;
        //}


        //#region API
        //[System.ComponentModel.DisplayName("API: Hasło")]
        //public string ApiPassword
        //{
        //    get { return GetProperyValue("api_token"); }  
        //    set { SetProperyValue("api_token", value); }
        //}

        //[System.ComponentModel.DisplayName("API: Url")]
        //public string ApiEndpoint
        //{
        //    get { return GetProperyValue("api_endpoint"); }
        //    set { SetProperyValue("api_endpoint", value); }
        //}

        //[System.ComponentModel.DisplayName("API: Użytkownik")]
        //public string ApiUser
        //{
        //    get { return GetProperyValue("api_user"); }
        //    set { SetProperyValue("api_user", value); }
        //}
        //#endregion


        //[System.ComponentModel.DisplayName("API: Użytkownik")]
        //public string UniqueParcles
        //{
        //    get { return GetProperyValue("app_qunique_parcels"); }
        //    set { SetProperyValue("app_qunique_parcels", value); }
        //}

        //[System.ComponentModel.DisplayName("Id czytnika")]
        //public string DeviceId
        //{
        //    get { return GetProperyValue("app_device_id"); }
        //    set { SetProperyValue("app_device_id", value); }
        //}
    }
}
