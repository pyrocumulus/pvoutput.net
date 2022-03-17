using System;
using NUnit.Framework;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Objects.Modules.Implementations;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using System.Collections.Generic;
using System.Linq;
using RichardSzalay.MockHttp;
using PVOutput.Net.Builders;
using PVOutput.Net.Objects.Modules;

namespace PVOutput.Net.Tests.Modules.Status
{
    public class AddBatchNetStatusRequestTests : BaseRequestsTest
    {
        private string[] GetSplitStatusPostLine(BatchNetStatusPost post) => AddBatchNetStatusRequest.FormatStatusPost(post).Split(',');

        [Test]
        public void Parameter_Timestamp_CreatesCorrectUriParameters()
        {
            var post = new BatchNetStatusPost() { Timestamp = new DateTime(2020, 2, 1, 13, 12, 20) };

            string[] postArray = GetSplitStatusPostLine(post);
            
            Assert.Multiple(() =>
            {
                Assert.That(postArray[0], Is.EqualTo("20200201"));
                Assert.That(postArray[1], Is.EqualTo("13:12"));
            });
        }

        [Test]
        public void Parameter_PowerExported_CreatesCorrectUriParameters()
        {
            var post = new BatchNetStatusPost() { PowerExported = 1111 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[3], Is.EqualTo("1111"));
        }

        [Test]
        public void Parameter_PowerImported_CreatesCorrectUriParameters()
        {
            var post = new BatchNetStatusPost() { PowerImported = 2222 };
            string[] postArray = GetSplitStatusPostLine(post);
            Assert.That(postArray[5], Is.EqualTo("2222"));
        }
    }
}
