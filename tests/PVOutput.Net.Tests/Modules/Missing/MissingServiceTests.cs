using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects;
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
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETMISSING_URL)
                        .WithQueryString("df=20160801&dt=20160830")
                        .RespondPlainText(MISSINGDATES_RESPONSE_SIMPLE);

            var response = await client.Missing.GetMissingDaysInPeriodAsync(new DateTime(2016, 8, 1), new DateTime(2016, 8, 30));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void MissingService_GetMissingDaysInPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Missing.GetMissingDaysInPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29));
            });
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
                Assert.That(result.Dates.Count(), Is.EqualTo(21));
                Assert.That(result.Dates.First(), Is.EqualTo(new DateTime(2016, 8, 1)));
                Assert.That(result.Dates.Last(), Is.EqualTo(new DateTime(2016, 8, 21)));
            });
        }

        [Test]
        public async Task MissingReader_ForEmptyResponse_CreatesCorrectObject()
        {
            IMissing result = await TestUtility.ExecuteObjectReaderByTypeAsync<IMissing>(MISSINGDATES_RESPONSE_NONE);
            Assert.That(result.Dates.Count(), Is.EqualTo(0));
        }
    }
}
