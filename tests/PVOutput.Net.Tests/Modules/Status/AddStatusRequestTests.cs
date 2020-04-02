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
    public class AddStatusRequestTests
    {
        [Test]
        public void Request_WithDate_CreatesCorrectUriParameters()
        {
            var request = new AddStatusRequest();
            request.StatusPost = new StatusPost() { Timestamp = new DateTime(2020, 2, 1, 13, 12, 20 )};

            var parameters = request.GetUriPathParameters();

            Assert.AreEqual("20200201", parameters["d"]);
            Assert.AreEqual("13:12", parameters["t"]);
        }
    }
}
