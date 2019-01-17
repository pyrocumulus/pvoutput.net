using System;
using System.Net.Http;

namespace PVOutput.Net.Requests.Handler
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly PVOutputClient _client;
        private HttpClient _httpClient;

        public HttpClientProvider(PVOutputClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public virtual HttpClient SetupHttpClient()
        {
            var httpClient = new HttpClient();
            SetupDefaultRequestHeaders(httpClient);
            return httpClient;
        }

        public virtual HttpClient GetHttpClient()
        {
            if (_httpClient == null)
                _httpClient = SetupHttpClient();

            return _httpClient;
        }

        protected void SetupDefaultRequestHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("X-Pvoutput-Apikey", _client.Apikey);
            httpClient.DefaultRequestHeaders.Add("X-Pvoutput-SystemId", _client.OwnedSystemId.ToString());
            httpClient.DefaultRequestHeaders.Add("X-Rate-Limit", "1");
        }
    }
}