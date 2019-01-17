using NUnit.Framework;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Requests.Handler
{
    public class TestHttpClientProvider : IHttpClientProvider
    {
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;

        public TestHttpClientProvider() 
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
        }

        public HttpClient GetHttpClient()
        {
            Assert.IsNotNull(_mockHttpMessageHandler);
            return new HttpClient(_mockHttpMessageHandler);
        }

        internal void SetupMockResponse(string uri, string responseContent)
        {
            _mockHttpMessageHandler.When(TestConstants.BASE_URL + uri)
                .Respond("text/plain", responseContent);
        }
    }
}
