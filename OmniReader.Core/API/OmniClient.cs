using Newtonsoft.Json;
using OmniReader.Data.Database.Model;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.API
{
    public class OmniClient : BaseClient
    {
        private const string URL_DATA    = "/data";
        private const string URL_USER    = "/user";
        private const string URL_SERVICE = "/service";
        
        public OmniClient(string userName, string userPassword, string endpoint) : base(endpoint)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("Error: Parameter 'userName' cannot be null or empty!");
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("Error: Parameter 'userPassword' cannot be null or empty!");
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("Error: Parameter 'endpoint' cannot be null or empty!");
            
            Authenticator = new HttpBasicAuthenticator(userName, userPassword);
        }
        
        /// <summary>
        /// POST
        /// </summary>
        /// <param name="scannedData"></param>
        public bool PushParcels(List<ScanData> scannedData)
        {
            if (scannedData == null || scannedData.Count == 0) throw new ArgumentNullException("Error: Parameter 'scannedData' cannot be null or empty!");

            ScanData[] toSend = scannedData.ToArray();
            
            RestRequest request = new RestRequest(URL_DATA, Method.POST);
            request.AddJsonBody(toSend);

            IRestResponse response = Execute(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                throw new Exception(response.ErrorMessage);
            }

            return response.StatusCode == System.Net.HttpStatusCode.OK ? true : false;
        }

        public List<T> GenericSync<T>(List<T> entities, string entitieSendpoint)
        {
            if (entities == null) throw new ArgumentNullException("Error: Parameter 'entities' cannot be null or empty!");

            List<T> ls = new List<T>();

            T[] toSend = entities.ToArray();

            RestRequest request = new RestRequest(entitieSendpoint, Method.POST);
            request.AddJsonBody(toSend);

            IRestResponse response = Execute(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                throw new Exception(response.ErrorMessage);
            }

            ls = JsonConvert.DeserializeObject<List<T>>(response.Content);

            return ls;
        }

        public List<AppUser> SyncUsersV2(List<AppUser> users)
        {
            return GenericSync(users, URL_USER);
        }


        public List<AdditionalService> SyncServicesV2(List<AdditionalService> services)
        {
            return GenericSync(services, URL_SERVICE);
        }


        public List<AppUser> SyncUsers(List<AppUser> users)
        {
            if (users == null) throw new ArgumentNullException("Error: Parameter 'users' cannot be null or empty!");

            List<AppUser> ls = new List<AppUser>();

            AppUser[] toSend = users.ToArray();

            RestRequest request = new RestRequest(URL_USER, Method.POST);
            request.AddJsonBody(toSend);

            IRestResponse response = Execute(request);
            if (!string.IsNullOrEmpty(response.ErrorMessage)) {
                throw new Exception(response.ErrorMessage);
            }

            ls = JsonConvert.DeserializeObject<List<AppUser>>(response.Content);

            return ls;
        }


        public List<AdditionalService> SyncService(List<AdditionalService> services)
        {
            if (services == null) throw new ArgumentNullException("Error: Parameter 'users' cannot be null or empty!");

            List<AdditionalService> ls = new List<AdditionalService>();

            AdditionalService[] toSend = services.ToArray();

            RestRequest request = new RestRequest(URL_USER, Method.POST);
            request.AddJsonBody(toSend);

            IRestResponse response = Execute(request);

            ls = JsonConvert.DeserializeObject<List<AdditionalService>>(response.Content);

            return ls;
        }
    }
}
