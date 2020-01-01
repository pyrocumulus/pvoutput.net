using System.Net.Http;
using NUnit.Framework;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Requests.Handler
{
    public class TestHttpClientProvider : IHttpClientProvider
    {
        internal MockHttpMessageHandler MockHttpMessageHandler { get; }

        public TestHttpClientProvider()
        {
            MockHttpMessageHandler = new MockHttpMessageHandler();
        }

        public HttpClient GetHttpClient()
        {
            Assert.IsNotNull(MockHttpMessageHandler);
            return new HttpClient(MockHttpMessageHandler);
        }

        internal void When(string uri, string responseContent)
        {
            MockHttpMessageHandler.When(TestConstants.BASE_URL + uri).Respond("text/plain", responseContent);
        }
    }
}
