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
            Assert.That(postArray[0], Is.EqualTo("20200201"));
            Assert.That(postArray[1], Is.EqualTo("13:12"));
        }

        [Test]
        public void Parameter_EnergyGeneration_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { EnergyGeneration = 1111 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[2], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_PowerGeneration_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { PowerGeneration = 2222 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[3], Is.EqualTo("2222"));
        }

        [Test]
        public void Parameter_EnergyConsumption_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { EnergyConsumption = 3333 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[4], Is.EqualTo("3333"));
        }

        [Test]
        public void Parameter_PowerConsumption_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { PowerConsumption = 4444 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[5], Is.EqualTo("4444"));
        }

        [Test]
        public void Parameter_Temperature_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { Temperature = 18.9m };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[6], Is.EqualTo("18.9"));
        }

        [Test]
        public void Parameter_Voltage_CreatesCorrectUriParameters()
        {
            var post = new StatusPost() { Voltage = 222.3m };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[7], Is.EqualTo("222.3"));
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
                Assert.That(postArray[8], Is.EqualTo("1"));
                Assert.That(postArray[9], Is.EqualTo("2"));
                Assert.That(postArray[10], Is.EqualTo("3"));
                Assert.That(postArray[11], Is.EqualTo("4"));
                Assert.That(postArray[12], Is.EqualTo("5"));
                Assert.That(postArray[13], Is.EqualTo("6"));
            });
        }
    }
}
