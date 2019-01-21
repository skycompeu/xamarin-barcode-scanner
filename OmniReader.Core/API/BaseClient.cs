using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.API
{
    public class BaseClient : RestClient
    {
        public BaseClient(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }
    }

}
