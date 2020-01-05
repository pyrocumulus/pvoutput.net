using System;
using System.Net.Http;

namespace PVOutput.Net.Requests.Handler
{
    public class HttpClientProvider : IHttpClientProvider
    {
        protected HttpClient _httpClient;

        public HttpClientProvider()
        {
        }

        public virtual HttpClient SetupHttpClient()
        {
            var httpClient = new HttpClient();
            return httpClient;
        }

        public virtual HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _httpClient = SetupHttpClient();
            }

            return _httpClient;
        }
    }
}
