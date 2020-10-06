using System.Net.Http;
using NUnit.Framework;
using PVOutput.Net.Requests;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Requests.Handler
{
    internal class TestHttpClientProvider : HttpClientProvider
    {
        internal MockHttpMessageHandler MockHttpMessageHandler { get; }

        public TestHttpClientProvider()
        {
            MockHttpMessageHandler = new MockHttpMessageHandler();
        }

        public override HttpClient SetupHttpClient()
        {
            Assert.That(MockHttpMessageHandler, Is.Not.Null);
            return MockHttpMessageHandler.ToHttpClient();
        }

        internal void When(string uri, string responseContent)
        {
            MockHttpMessageHandler.When(TestConstants.BASE_URL + uri).Respond("text/plain", responseContent);
        }
    }
}
