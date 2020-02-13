using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Supply
{
    [TestFixture]
    public partial class SupplyServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task SupplyService_WithNoParameters_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETSUPPLY_URL)
                        .RespondPlainText("");

            var response = await client.Supply.GetSupplyAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task SupplyService_WithTimezone_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETSUPPLY_URL)
                        .WithQueryString("tz=Europe/London")
                        .RespondPlainText("");

            var response = await client.Supply.GetSupplyAsync("Europe/London");
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task SupplyService_WithRegionKey_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETSUPPLY_URL)
                        .WithQueryString("r=1:victoria")
                        .RespondPlainText("");

            var response = await client.Supply.GetSupplyAsync(regionKey: "1:victoria");
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task SupplyReader_ForResponse_CreatesCorrectObject()
        {
            ISupply result = await TestUtility.ExecuteObjectReaderByTypeAsync<ISupply>(SUPPLY_RESPONSE_SINGLE);

            Assert.Multiple(() =>
            {
                // 2012-10-31T19:50:00+1000
                var testDateTime = new DateTimeOffset(2012, 10, 31, 19, 50, 0, new TimeSpan(10, 0, 0));

                Assert.AreEqual(testDateTime, result.Timestamp);
                Assert.AreEqual("Western Australia", result.RegionName);
                Assert.AreEqual(5.709m, result.Utilisation);
                Assert.AreEqual(19108, result.TotalPowerOutput);
                Assert.AreEqual(11598, result.TotalPowerInput);
                Assert.AreEqual(239, result.AveragePowerOutput);
                Assert.AreEqual(892, result.AveragePowerInput);
                Assert.AreEqual(-653, result.AverageNetPower);
                Assert.AreEqual(80, result.SystemsOut);
                Assert.AreEqual(13, result.SystemsIn);
                Assert.AreEqual(334719, result.TotalSize);
                Assert.AreEqual(4184, result.AverageSize);
            });
        }
    }
}
