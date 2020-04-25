using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Objects.Modules.Implementations;
using PVOutput.Net.Requests.Handler;
using Tavis.UriTemplates;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public class AddStatusRequestTests
    {
        private AddStatusRequest CreateRequestWithPost(StatusPost status)
        {
            return new AddStatusRequest() { StatusPost = status };
        }

        [Test]
        public void Parameter_Timestamp_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { Timestamp = new DateTime(2020, 2, 1, 13, 12, 20) });

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("20200201", parameters["d"]);
            Assert.AreEqual("13:12", parameters["t"]);
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { EnergyGeneration = 1111 });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(1111, parameters["v1"]);
        }

        [Test]
        public void Parameter_PowerGeneration_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { PowerGeneration = 2222 });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(2222, parameters["v2"]);
        }

        [Test]
        public void Parameter_EnergyConsumption_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { EnergyConsumption = 3333 });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(3333, parameters["v3"]);
        }

        [Test]
        public void Parameter_PowerConsumption_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { PowerConsumption = 4444 });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(4444, parameters["v4"]);
        }

        [Test]
        public void Parameter_Temperature_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { Temperature = 18.9m });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("18.9", parameters["v5"]);
        }

        [Test]
        public void Parameter_Voltage_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { Voltage = 222.3m });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("222.3", parameters["v6"]);
        }

        [Test]
        public void Parameter_Cumulative_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { Cumulative = Enums.CumulativeStatusType.LifetimeGeneration });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(2, parameters["c1"]);
        }

        [Test]
        public void Parameter_Net_CreatesCorrectUriParameters()
        {
            var request= CreateRequestWithPost(new StatusPost() { Net = true });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(1, parameters["n"]);
        }

        [Test]
        public void Parameter_TextMessage_CreatesCorrectUriParameters()
        {
            var request = CreateRequestWithPost(new StatusPost() { TextMessage = "Text message" });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("Text message", parameters["m1"]);
        }

        [Test]
        public void Parameter_ExtendedValues_CreatesCorrectUriParameters()
        {
            var request = new AddStatusRequest()
            {
                StatusPost = new StatusPost()
                {
                    ExtendedValue1 = 1,
                    ExtendedValue2 = 2,
                    ExtendedValue3 = 3,
                    ExtendedValue4 = 4,
                    ExtendedValue5 = 5,
                    ExtendedValue6 = 6
                }
            };
            var parameters = request.GetUriPathParameters();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("1", parameters["v7"]);
                Assert.AreEqual("2", parameters["v8"]);
                Assert.AreEqual("3", parameters["v9"]);
                Assert.AreEqual("4", parameters["v10"]);
                Assert.AreEqual("5", parameters["v11"]);
                Assert.AreEqual("6", parameters["v12"]);
            });
        }
    }
}
