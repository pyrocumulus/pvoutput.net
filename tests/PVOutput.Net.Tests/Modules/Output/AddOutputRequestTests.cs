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
            Assert.AreEqual("20200301", parameters["d"]);
        }

        [Test]
        public void Parameter_EnergyGenerated_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { EnergyGenerated = 5500 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(5500, parameters["g"]);
        }

        [Test]
        public void Parameter_EnergyExported_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { EnergyExported = 2500 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(2500, parameters["e"]);
        }


        [Test]
        public void Parameter_PeakPower_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakPower = 3700 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(3700, parameters["pp"]);
        }

        [Test]
        public void Parameter_PeakTime_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakTime = new DateTime (2020, 3, 1, 10, 30, 12) } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("10:30", parameters["pt"]);
        }

        [Test]
        public void Parameter_Condition_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Condition = Enums.WeatherCondition.MostlyCloudy } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("Mostly Cloudy", parameters["cd"]);
        }

        [Test]
        public void Parameter_MinimumTemperature_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { MinimumTemperature = 9.1m } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("9.1", parameters["tm"]);
        }

        [Test]
        public void Parameter_MaximumTemperature_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { MaximumTemperature = 18.2m } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("18.2", parameters["tx"]);
        }

        [Test]
        public void Parameter_Comments_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Comments = "Comment" } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("Comment", parameters["cm"]);
        }


        [Test]
        public void Parameter_PeakEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { PeakEnergyImport = 223 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("223", parameters["ip"]);
        }

        [Test]
        public void Parameter_OffPeakEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { OffPeakEnergyImport = 224 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("224", parameters["io"]);
        }

        [Test]
        public void Parameter_ShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { ShoulderEnergyImport = 225 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("225", parameters["is"]);
        }

        [Test]
        public void Parameter_HighShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { HighShoulderEnergyImport = 226 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("226", parameters["ih"]);
        }


        [Test]
        public void Parameter_Consumption_CreatesCorrectUriParameters()
        {
            var request = new AddOutputRequest() { Output = new OutputPost() { Consumption = 9876 } };

            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(9876, parameters["c"]);
        }

        // TODO: Add unit tests for AddBatchOutputRequest and it's FormatOutputs() method.
    }
}
