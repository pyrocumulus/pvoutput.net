using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace PVOutput.Net.Requests
{
    internal class HttpClientProvider : IHttpClientProvider
    {
        protected HttpClient _httpClient = null!;

        public HttpClientProvider()
        {
        }

        public virtual HttpClient SetupHttpClient()
        {
            return new HttpClient();
        }

        [MemberNotNull(nameof(_httpClient))]
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
