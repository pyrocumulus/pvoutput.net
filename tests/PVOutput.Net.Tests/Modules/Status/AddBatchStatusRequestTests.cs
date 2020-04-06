using System;
using NUnit.Framework;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Tests.Modules.Status
{
    public class AddBatchStatusRequestTests
    {
        private string[] GetSplitStatusPostLine(StatusPost post) => AddBatchStatusRequest.FormatStatusPost(post).Split(',');

        [Test]
        public void Parameter_Timestamp_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { Timestamp = new DateTime(2020, 2, 1, 13, 12, 20) };

            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("20200201", postArray[0]);
            Assert.AreEqual("13:12", postArray[1]);
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { EnergyGeneration = 1111 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("1111", postArray[2]);
        }

        [Test]
        public void Parameter_PowerGeneration_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { PowerGeneration = 2222 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("2222", postArray[3]);
        }

        [Test]
        public void Parameter_EnergyConsumption_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { EnergyConsumption = 3333 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("3333", postArray[4]);
        }

        [Test]
        public void Parameter_PowerConsumption_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { PowerConsumption = 4444 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("4444", postArray[5]);
        }

        [Test]
        public void Parameter_Temperature_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { Temperature = 18.9m };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("18.9", postArray[6]);
        }

        [Test]
        public void Parameter_Voltage_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { Voltage = 222.3m };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.AreEqual("222.3", postArray[7]);
        }

        [Test]
        public void Parameter_ExtendedValues_CreatesCorrectUriParameters()
        {
            var post = new StatusPost()
            {
                ExtendedValue1 = 1,
                ExtendedValue2 = 2,
                ExtendedValue3 = 3,
                ExtendedValue4 = 4,
                ExtendedValue5 = 5,
                ExtendedValue6 = 6
            };
            string[] postArray = GetSplitStatusPostLine(post);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("1", postArray[8]);
                Assert.AreEqual("2", postArray[9]);
                Assert.AreEqual("3", postArray[10]);
                Assert.AreEqual("4", postArray[11]);
                Assert.AreEqual("5", postArray[12]);
                Assert.AreEqual("6", postArray[13]);
            });
        }
    }
}
