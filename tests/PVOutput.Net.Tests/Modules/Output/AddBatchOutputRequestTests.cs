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
        private string[] GetSplitOutputPostLine(BatchOutputPost post) => AddBatchOutputRequest.FormatOutputPost(post).Split(',');

        [Test]
        public void Parameter_Timestamp_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { OutputDate = new DateTime(2020, 2, 1, 13, 12, 20) };

            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[0], Is.EqualTo("20200201"));
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyGenerated = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[1], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_EnergyExported_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyExported = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[2], Is.EqualTo("2222"));
        }

        [Test]
        public void Parameter_EnergyUsed_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyUsed = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[3], Is.EqualTo("3333"));
        }

        [Test]
        public void Parameter_PeakPower_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakPower = 4444 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[4], Is.EqualTo("4444"));
        }

        [Test]
        public void Parameter_PeakTime_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakTime = new TimeSpan(13, 12, 20) };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[5], Is.EqualTo("13:12"));
        }

        [Test]
        public void Parameter_WeatherCondition_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { Condition = Enums.WeatherCondition.PartlyCloudy };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[6], Is.EqualTo("Partly Cloudy"));
        }

        [Test]
        public void Parameter_MinimumTemperature_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { MinimumTemperature = 9.2m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[7], Is.EqualTo("9.2"));
        }

        [Test]
        public void Parameter_MaximumTemperature_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { MaximumTemperature = 18.9m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[8], Is.EqualTo("18.9"));
        }

        [Test]
        public void Parameter_Comments_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { Comments = "Comment" };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[9], Is.EqualTo("Comment"));
        }

        [Test]
        public void Parameter_PeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakEnergyImport = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[10], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_OffPeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { OffPeakEnergyImport = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[11], Is.EqualTo("2222"));
        }

        [Test]
        public void Parameter_ShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { ShoulderEnergyImport = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.That(postArray[12], Is.EqualTo("3333"));
        }
    }
}
