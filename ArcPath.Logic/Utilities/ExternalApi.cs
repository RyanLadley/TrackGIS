using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArcPath.Logic.Utilities
{
    /// <summary>
    /// Handles the sending of requests to an external api.
    /// </summary>
    public class ExternalApi : IDisposable
    {
        //Best Practice of Http Client is not to create a new one for each request: https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/
        private HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExternalApi(string baseUrl, Dictionary<string, string> defaultHeaders = null)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();

            //Add the provided headers to the http client
            if (defaultHeaders != null)
            {
                foreach (var header in defaultHeaders)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Preforms a GET request to the provided url. Converts the provided parameters to a query string
        /// </summary>
        public Task<HttpResponseMessage> Get(string url, Dictionary<string, string> parameters = null)
        {
            return _httpClient.GetAsync($"{_baseUrl}/{url}{_toQueryString(parameters)}");
        }

        /// <summary>
        /// /// Preforms a POST  request to the provided url. Converts the payload into Form Content and adds it to the body of the request 
        /// </summary>
        public Task<HttpResponseMessage> Post(string url, List<KeyValuePair<string, string>> payload)
        {
            var content = new FormUrlEncodedContent(payload);
            return _httpClient.PostAsync($"{_baseUrl}/{url}", content);
        }

        /// <summary>
        /// Preforms a POST  request to the provided url. Converts the payload into a JSON object and adds it to the body of the request 
        /// </summary>
        public Task<HttpResponseMessage> Post(string url, object payload)
        {
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            return _httpClient.PostAsync($"{_baseUrl}/{url}", content);
        }

        /// <summary>
        /// Preforms a POST  request to the provided url. Converts the provided parameters to a query string
        /// </summary>
        public Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters)
        {
            var test = $"{_baseUrl}/{url}{_toQueryString(parameters)}";
            return _httpClient.PostAsync($"{_baseUrl}/{url}{_toQueryString(parameters)}", null);
        }

        private string _toQueryString(Dictionary<string, string> dict)
        {
            if (dict == null)
                return string.Empty;

            var queryList = new List<string>();

            foreach (var pair in dict)
            {
                queryList.Add($"{pair.Key}={pair.Value}");
            }

            return "?" + string.Join("&", queryList.ToArray());
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
