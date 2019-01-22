using NUnit.Framework;
using PVOutput.Net.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public class SystemModuleTests
    {
        [Test]
        public async Task SystemService_GetOwnSystem()
        {
            var client = TestUtility.GetMockClient(SystemModuleTestsData.GETSYSTEM_URL, SystemModuleTestsData.SYSTEM_RESPONSE_EXTENDED);
            var response = await client.System.GetOwnSystem();

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var system = response.Value;
            Assert.AreEqual("Test System", system.SystemName);
        }
    }
}
