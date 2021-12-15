using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PVOutput.Net.Objects.Modules.Implementations;
using PVOutput.Net.Requests.Modules;

namespace PVOutput.Net.Tests.Modules.Output
{
    public class AddBatchStatusRequestTests
    {
        private string[] GetSplitOutputPostLine(OutputPost post) => AddOutputsRequest.FormatOutputPost(post).Split(',');

        [Test]
        public void Parameter_Timestamp_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { OutputDate = new DateTime(2020, 2, 1, 13, 12, 20) };

            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[0], Is.EqualTo("20200201"));
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { EnergyGenerated = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[1], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_EnergyExported_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { EnergyExported = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[2], Is.EqualTo("2222"));
        }

        [Test]
        public void Parameter_PeakPower_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { PeakPower = 4444 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[3], Is.EqualTo("4444"));
        }

        [Test]
        public void Parameter_PeakTime_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { PeakTime = new TimeSpan(13, 12, 20) };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[4], Is.EqualTo("13:12"));
        }

        [Test]
        public void Parameter_WeatherCondition_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { Condition = Enums.WeatherCondition.PartlyCloudy };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[5], Is.EqualTo("Partly Cloudy"));
        }

        [Test]
        public void Parameter_MinimumTemperature_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { MinimumTemperature = 9.2m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[6], Is.EqualTo("9.2"));
        }

        [Test]
        public void Parameter_MaximumTemperature_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { MaximumTemperature = 18.9m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[7], Is.EqualTo("18.9"));
        }

        [Test]
        public void Parameter_Comments_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { Comments = "Comment" };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[8], Is.EqualTo("Comment"));
        }

        [Test]
        public void Parameter_PeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { PeakEnergyImport = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[9], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_OffPeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { OffPeakEnergyImport = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[10], Is.EqualTo("2222"));
        }

        [Test]
        public void Parameter_ShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { ShoulderEnergyImport = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[11], Is.EqualTo("3333"));
        }

        [Test]
        public void Parameter_HighShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { HighShoulderEnergyImport = 5555 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[12], Is.EqualTo("5555"));
        }

        [Test]
        public void Parameter_EnergyUsed_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { Consumption = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[13], Is.EqualTo("3333"));
        }

        [Test]
        public void Parameter_PeakEnergyExport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { PeakEnergyExport = 7777 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[14], Is.EqualTo("7777"));
        }

        [Test]
        public void Parameter_OffPeakEnergyExport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { OffPeakEnergyExport = 6666 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[15], Is.EqualTo("6666"));
        }

        [Test]
        public void Parameter_ShoulderEnergyExport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { ShoulderEnergyExport = 8888 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[16], Is.EqualTo("8888"));
        }

        [Test]
        public void Parameter_HighShoulderEnergyExport_CreatesCorrectUriParameters()
        {
            var post = new OutputPost() { HighShoulderEnergyExport = 9999 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[17], Is.EqualTo("9999"));
        }
    }
}
