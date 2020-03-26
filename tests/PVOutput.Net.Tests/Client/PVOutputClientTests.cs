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
                Assert.AreEqual("apikey", client.ApiKey);
                Assert.AreEqual(1234, client.OwnedSystemId);
                Assert.AreEqual(NullLogger<PVOutputClient>.Instance, client.Logger);
            });
        }

        [Test]
        public void Create_ClientWithLogger_CreatesClientWithLogger()
        {
            var loggerSubstitute = Substitute.For<ILogger<PVOutputClient>>();
            var client = new PVOutputClient("apikey", 1234, loggerSubstitute);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("apikey", client.ApiKey);
                Assert.AreEqual(1234, client.OwnedSystemId);
                Assert.AreNotEqual(NullLogger<PVOutputClient>.Instance, client.Logger);
            });
        }


        [Test]
        public void Create_ClientWithValidOptions_CreatesCorrectClient()
        {
            var client = new PVOutputClient(new PVOutputClientOptions() { ApiKey = "apikey", OwnedSystemId = 1234 });

            Assert.Multiple(() =>
            {
                Assert.AreEqual("apikey", client.ApiKey);
                Assert.AreEqual(1234, client.OwnedSystemId);
            });
        }

        [Test]
        public void Create_ClientWithOptionsAndLogger_CreatesClientWithLogger()
        {
            var loggerSubstitute = Substitute.For<ILogger<PVOutputClient>>();
            var client = new PVOutputClient(new PVOutputClientOptions() { ApiKey = "apikey", OwnedSystemId = 1234 }, loggerSubstitute);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("apikey", client.ApiKey);
                Assert.AreEqual(1234, client.OwnedSystemId);
                Assert.AreNotEqual(NullLogger<PVOutputClient>.Instance, client.Logger);
            });
        }
    }
}
