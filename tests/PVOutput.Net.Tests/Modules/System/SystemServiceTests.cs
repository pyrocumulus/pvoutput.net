using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public class SystemServiceTests
    {
        [Test]
        public async Task SystemService_GetOwnSystem()
        {
            var client = TestUtility.GetMockClient(SystemSystemTestsData.GETSYSTEM_URL, SystemSystemTestsData.SYSTEM_RESPONSE_EXTENDED);
            var response = await client.System.GetOwnSystem();

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var system = response.Value;
            Assert.AreEqual("Test System", system.SystemName);

            Assert.AreEqual(1, system.Donations);
            Assert.AreEqual(10.65, system.ImportDailyCharge);
            Assert.AreEqual("DC-2 Voltage", system.ExtendedDataConfig[1].Label);
            Assert.AreEqual(159, system.MonthlyGenerationEstimates[PVMonth.October]);
            Assert.AreEqual(0, system.MonthlyConsumptionEstimates[PVMonth.January]);
        }
    }
}
