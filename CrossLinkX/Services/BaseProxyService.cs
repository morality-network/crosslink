using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrossLinkX.Services
{
    public abstract class BaseProxyService
    {
        protected readonly HttpClient _httpClient;
        public BaseProxyService(HttpClient client)
        {
            _httpClient = client;
        }

        protected async Task<T> PostStringAsync<T>(string model, List<KeyValuePair<string, string>> additionalHeaders = null)
        {
            var content = new StringContent(model, Encoding.UTF8, "application/json");
            AddHeaders(content, additionalHeaders);
            var response = await _httpClient.PostAsync(string.Empty, content);
            var responseAsString = await response?.Content?.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseAsString);
        }

        private void AddHeaders(HttpContent content, List<KeyValuePair<string, string>> additionalHeaders = null)
        {
            if (additionalHeaders == null)
                additionalHeaders = new List<KeyValuePair<string, string>>();

            foreach (var header in additionalHeaders)
            {
                content.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
