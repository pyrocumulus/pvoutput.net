using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using PVOutput.Net.Requests;

namespace PVOutput.Net.Tests.Handler
{
    [TestFixture]
    public class HttpClientProviderTests
    {
        [Test]
        public void GetHttpClient_Returns_HttpClient()
        {
            var provider = new HttpClientProvider();
            HttpClient client = provider.GetHttpClient();

            Assert.IsNotNull(client);
        }

        [Test]
        public void SetupHttpClient_Returns_HttpClient()
        {
            var provider = new HttpClientProvider();
            HttpClient client = provider.SetupHttpClient();

            Assert.IsNotNull(client);
        }

        [Test]
        public void GetHttpClient_ReturnsSame_HttpClient()
        {
            var provider = new HttpClientProvider();
            HttpClient client = provider.GetHttpClient();

            HttpClient secondClient = provider.GetHttpClient();
            Assert.AreSame(client, secondClient);
        }
    }
}
