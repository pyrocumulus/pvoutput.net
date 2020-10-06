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

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public class AddOutputRequestTests
    {
        [Test]
        public void Parameter_Date_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { OutputDate = new DateTime(2020, 3, 1) } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["d"], Is.EqualTo("20200301"));
        }

        [Test]
        public void Parameter_EnergyGenerated_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { EnergyGenerated = 5500 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["g"], Is.EqualTo(5500));
        }

        [Test]
        public void Parameter_EnergyExported_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { EnergyExported = 2500 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["e"], Is.EqualTo(2500));
        }


        [Test]
        public void Parameter_PeakPower_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakPower = 3700 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["pp"], Is.EqualTo(3700));
        }

        [Test]
        public void Parameter_PeakTime_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakTime = new TimeSpan(10, 30, 12) } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["pt"], Is.EqualTo("10:30"));
        }

        [Test]
        public void Parameter_Condition_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Condition = Enums.WeatherCondition.MostlyCloudy } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["cd"], Is.EqualTo("Mostly Cloudy"));
        }

        [Test]
        public void Parameter_MinimumTemperature_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { MinimumTemperature = 9.1m } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["tm"], Is.EqualTo("9.1"));
        }

        [Test]
        public void Parameter_MaximumTemperature_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { MaximumTemperature = 18.2m } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["tx"], Is.EqualTo("18.2"));
        }

        [Test]
        public void Parameter_Comments_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Comments = "Comment" } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["cm"], Is.EqualTo("Comment"));
        }


        [Test]
        public void Parameter_PeakEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakEnergyImport = 223 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["ip"], Is.EqualTo("223"));
        }

        [Test]
        public void Parameter_OffPeakEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { OffPeakEnergyImport = 224 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["io"], Is.EqualTo("224"));
        }

        [Test]
        public void Parameter_ShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { ShoulderEnergyImport = 225 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["is"], Is.EqualTo("225"));
        }

        [Test]
        public void Parameter_HighShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { HighShoulderEnergyImport = 226 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["ih"], Is.EqualTo("226"));
        }


        [Test]
        public void Parameter_Consumption_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Consumption = 9876 } };

            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["c"], Is.EqualTo(9876));
        }

        // TODO: Add unit tests for AddBatchOutputRequest and it's FormatOutputs() method.
    }
}
