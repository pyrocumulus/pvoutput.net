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

                Assert.That(result.Timestamp, Is.EqualTo(testDateTime));
                Assert.That(result.RegionName, Is.EqualTo("Western Australia"));
                Assert.That(result.Utilisation, Is.EqualTo(5.709m));
                Assert.That(result.TotalPowerOutput, Is.EqualTo(19108));
                Assert.That(result.TotalPowerInput, Is.EqualTo(11598));
                Assert.That(result.AveragePowerOutput, Is.EqualTo(239));
                Assert.That(result.AveragePowerInput, Is.EqualTo(892));
                Assert.That(result.AverageNetPower, Is.EqualTo(-653));
                Assert.That(result.SystemsOut, Is.EqualTo(80));
                Assert.That(result.SystemsIn, Is.EqualTo(13));
                Assert.That(result.TotalSize, Is.EqualTo(334719));
                Assert.That(result.AverageSize, Is.EqualTo(4184));
            });
        }
    }
}
