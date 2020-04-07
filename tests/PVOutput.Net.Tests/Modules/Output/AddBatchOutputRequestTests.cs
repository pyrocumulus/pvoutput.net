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
            Assert.AreEqual("20200201", postArray[0]);
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyGenerated = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("1111", postArray[1]);
        }

        [Test]
        public void Parameter_EnergyExported_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyExported = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("2222", postArray[2]);
        }

        [Test]
        public void Parameter_EnergyUsed_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { EnergyUsed = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("3333", postArray[3]);
        }

        [Test]
        public void Parameter_PeakPower_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakPower = 4444 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("4444", postArray[4]);
        }

        [Test]
        public void Parameter_PeakTime_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakTime = new DateTime(2020, 2, 1, 13, 12, 20) };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("13:12", postArray[5]);
        }

        [Test]
        public void Parameter_WeatherCondition_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { Condition = Enums.WeatherCondition.PartlyCloudy };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("Partly Cloudy", postArray[6]);
        }

        [Test]
        public void Parameter_MinimumTemperature_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { MinimumTemperature = 9.2m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("9.2", postArray[7]);
        }

        [Test]
        public void Parameter_MaximumTemperature_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { MaximumTemperature = 18.9m };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("18.9", postArray[8]);
        }

        [Test]
        public void Parameter_Comments_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { Comments = "Comment" };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("Comment", postArray[9]);
        }

        [Test]
        public void Parameter_PeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { PeakEnergyImport = 1111 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("1111", postArray[10]);
        }

        [Test]
        public void Parameter_OffPeakEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { OffPeakEnergyImport = 2222 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("2222", postArray[11]);
        }

        [Test]
        public void Parameter_ShoulderEnergyImport_CreatesCorrectUriParameters()
        {
            var post = new BatchOutputPost() { ShoulderEnergyImport = 3333 };
            string[] postArray = GetSplitOutputPostLine(post);
            Assert.AreEqual("3333", postArray[12]);
        }
    }
}
