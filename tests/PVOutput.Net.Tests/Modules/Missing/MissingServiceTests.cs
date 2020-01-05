using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Missing
{
    [TestFixture]
    public partial class MissingServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task MissingService_GetMissingDaysInPeriod_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETMISSING_URL)
                        .WithQueryString("df=20160801&dt=20160830")
                        .RespondPlainText(MISSINGDATES_RESPONSE_SIMPLE);

            var response = await client.Missing.GetMissingDaysInPeriod(new DateTime(2016, 8, 1), new DateTime(2016, 8, 30));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task MissingReader_ForResponse_CreatesCorrectObject()
        {
            IMissing result = await TestUtility.ExecuteObjectReaderByTypeAsync<IMissing>(MISSINGDATES_RESPONSE_SIMPLE);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(21, result.Dates.Count());
                Assert.AreEqual(new DateTime(2016, 8, 1), result.Dates.First());
                Assert.AreEqual(new DateTime(2016, 8, 21), result.Dates.Last());
            });
        }

        [Test]
        public async Task MissingReader_ForEmptyResponse_CreatesCorrectObject()
        {
            IMissing result = await TestUtility.ExecuteObjectReaderByTypeAsync<IMissing>(MISSINGDATES_RESPONSE_NONE);
            Assert.AreEqual(0, result.Dates.Count());
        }
    }
}
