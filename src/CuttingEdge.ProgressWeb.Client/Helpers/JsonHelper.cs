using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CuttingEdge.ProgressWeb.Client.Helpers
{
    public class JsonHelper : IDisposable
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public JsonHelper(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _httpClient = new HttpClient();
        }

        public string GetServiceUrl(string serviceName)
        {
            return $"{_baseUrl}/api/{serviceName}";
        }


        public T Get<T>(string serviceName)
        {
            return JsonConvert.DeserializeObject<T>(Get(serviceName));
        }

        public string Get(string serviceName)
        {
            var response = _httpClient.GetAsync(GetServiceUrl(serviceName)).Result;
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }
        

        public async Task<T> GetAsync<T>(string serviceName)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(serviceName));
        }

        public async Task<string> GetAsync(string serviceName)
        {
            var response = await _httpClient.GetAsync(GetServiceUrl(serviceName));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }


        public T Get<T>(string serviceName, object key)
        {
            return JsonConvert.DeserializeObject<T>(Get(serviceName, key));
        }

        public string Get(string serviceName, object key)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetServiceUrl(serviceName));
            request.Properties.Add("id", key);
            var response = _httpClient.SendAsync(request).Result;

            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }


        public async Task<T> GetAsync<T>(string serviceName, object key)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(serviceName, key));
        }

        public async Task<string> GetAsync(string serviceName, object key)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, GetServiceUrl(serviceName));
            request.Properties.Add("id", key);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
