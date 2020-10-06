using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NUnit.Framework;
using PVOutput.Net.DependencyInjection;

namespace PVOutput.Net.Tests.Client
{
    [TestFixture]
    public class PVOutputClientTests
    {
        [Test]
        public void Create_ClientWithValidParameters_CreatesCorrectClient()
        {
            var client = new PVOutputClient("apikey", 1234);

            Assert.Multiple(() => 
            {
                Assert.That(client.ApiKey, Is.EqualTo("apikey"));
                Assert.That(client.OwnedSystemId, Is.EqualTo(1234));
                Assert.That(client.Logger, Is.EqualTo(NullLogger<PVOutputClient>.Instance));
                Assert.That(client.HttpClientProvider, Is.Not.Null);
            });
        }

        [Test]
        public void Create_ClientWithLogger_CreatesClientWithLogger()
        {
            var loggerSubstitute = Substitute.For<ILogger<PVOutputClient>>();
            var client = new PVOutputClient("apikey", 1234, loggerSubstitute);

            Assert.Multiple(() =>
            {
                Assert.That(client.ApiKey, Is.EqualTo("apikey"));
                Assert.That(client.OwnedSystemId, Is.EqualTo(1234));
                Assert.That(client.Logger, Is.Not.EqualTo(NullLogger<PVOutputClient>.Instance));
            });
        }

        [Test]
        public void Create_ClientWithValidOptions_CreatesCorrectClient()
        {
            var client = new PVOutputClient(new PVOutputClientOptions() { ApiKey = "apikey", OwnedSystemId = 1234 });

            Assert.Multiple(() =>
            {
                Assert.That(client.ApiKey, Is.EqualTo("apikey"));
                Assert.That(client.OwnedSystemId, Is.EqualTo(1234));
            });
        }

        [Test]
        public void Create_ClientWithOptionsAndLogger_CreatesClientWithLogger()
        {
            var loggerSubstitute = Substitute.For<ILogger<PVOutputClient>>();
            var client = new PVOutputClient(new PVOutputClientOptions() { ApiKey = "apikey", OwnedSystemId = 1234 }, loggerSubstitute);

            Assert.Multiple(() =>
            {
                Assert.That(client.ApiKey, Is.EqualTo("apikey"));
                Assert.That(client.OwnedSystemId, Is.EqualTo(1234));
                Assert.That(client.Logger, Is.Not.EqualTo(NullLogger<PVOutputClient>.Instance));
            });
        }
    }
}
