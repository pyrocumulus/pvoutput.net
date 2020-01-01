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
    public partial class MissingServiceTests
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

            Assert.Multiple(() =>
            {
                Assert.IsNull(response.Exception);
                Assert.IsTrue(response.HasValue);
                Assert.IsNotNull(response.IsSuccess);
                Assert.AreEqual(21, response.Value.Dates.Count());

            });
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task MissingReader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IMissing>();
            IMissing result = await reader.ReadObjectAsync(new StringReader(MISSINGDATES_RESPONSE_SIMPLE));

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
            var reader = StringFactoryContainer.CreateObjectReader<IMissing>();
            IMissing result = await reader.ReadObjectAsync(new StringReader(MISSINGDATES_RESPONSE_NONE));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, result.Dates.Count());
            });
        }
    }
}
