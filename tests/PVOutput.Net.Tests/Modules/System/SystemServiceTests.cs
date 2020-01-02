using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public partial class SystemServiceTests
    {
        // TODO: not all possible calls on the system service are being tested right now

        [Test]
        public async Task SystemService_GetOwnSystem_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETSYSTEM_URL)
                        .RespondPlainText(SYSTEM_RESPONSE_EXTENDED);

            var response = await client.System.GetOwnSystem();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.IsNull(response.Exception);
            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Value);
        }

        /*
         * Deserialisation tests below
         */

        // TODO: not all aspects are being tested right now

        [Test]
        public async Task SystemReader_ForResponse_CreatesCorrectObject()
        {
            ISystem result = await TestUtility.ExecuteObjectReaderByTypeAsync<ISystem>(SYSTEM_RESPONSE_EXTENDED);

            Assert.AreEqual("Test System", result.SystemName);
            Assert.AreEqual(1, result.Donations);
            Assert.AreEqual(10.65, result.ImportDailyCharge);
            Assert.AreEqual("DC-2 Voltage", result.ExtendedDataConfig[1].Label);
            Assert.AreEqual(159, result.MonthlyGenerationEstimates[PVMonth.October]);
            Assert.AreEqual(0, result.MonthlyConsumptionEstimates[PVMonth.January]);
        }
    }
}
