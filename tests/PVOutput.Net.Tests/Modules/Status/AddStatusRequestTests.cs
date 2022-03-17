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
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { Timestamp = new DateTime(2020, 2, 1, 13, 12, 20) });

            IDictionary<string, object> parameters = request.GetUriPathParameters();
            
            Assert.Multiple(() =>
            {
                Assert.That(parameters["d"], Is.EqualTo("20200201"));
                Assert.That(parameters["t"], Is.EqualTo("13:12"));
            });
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { EnergyGeneration = 1111 });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v1"], Is.EqualTo(1111));
        }

        [Test]
        public void Parameter_PowerGeneration_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { PowerGeneration = 2222 });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v2"], Is.EqualTo(2222));
        }

        [Test]
        public void Parameter_EnergyConsumption_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { EnergyConsumption = 3333 });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v3"], Is.EqualTo(3333));
        }

        [Test]
        public void Parameter_PowerConsumption_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { PowerConsumption = 4444 });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v4"], Is.EqualTo(4444));
        }

        [Test]
        public void Parameter_Temperature_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { Temperature = 18.9m });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v5"], Is.EqualTo("18.9"));
        }

        [Test]
        public void Parameter_Voltage_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { Voltage = 222.3m });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["v6"], Is.EqualTo("222.3"));
        }

        [Test]
        public void Parameter_Cumulative_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { Cumulative = Enums.CumulativeStatusType.LifetimeGeneration });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["c1"], Is.EqualTo(2));
        }

        [Test]
        public void Parameter_Net_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { Net = true });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["n"], Is.EqualTo(1));
        }

        [Test]
        public void Parameter_TextMessage_CreatesCorrectUriParameters()
        {
            AddStatusRequest request = CreateRequestWithPost(new StatusPost() { TextMessage = "Text message" });
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["m1"], Is.EqualTo("Text message"));
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
            IDictionary<string, object> parameters = request.GetUriPathParameters();

            Assert.Multiple(() =>
            {
                Assert.That(parameters["v7"], Is.EqualTo("1"));
                Assert.That(parameters["v8"], Is.EqualTo("2"));
                Assert.That(parameters["v9"], Is.EqualTo("3"));
                Assert.That(parameters["v10"], Is.EqualTo("4"));
                Assert.That(parameters["v11"], Is.EqualTo("5"));
                Assert.That(parameters["v12"], Is.EqualTo("6"));
            });
        }
    }
}
