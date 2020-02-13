using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Insolation
{
    [TestFixture]
    public partial class InsolationServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task InsolationService_GetForSystem_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETINSOLATION_URL)
                        .WithQueryString("sid1=54321")
                        .RespondPlainText(INSOLATION_RESPONSE_BASIC);

            var response = await client.Insolation.GetInsolationForSystem(54321);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task InsolationReader_ForResponse_CreatesCorrectObject()
        {
            IEnumerable<IInsolation> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IInsolation>(INSOLATION_RESPONSE_BASIC);
            var firstInsolation = result.First();
            var lastInsolation = result.Last();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, firstInsolation.Energy);
                Assert.AreEqual(0, firstInsolation.Power);
                Assert.AreEqual(DateTime.Today.Add(new TimeSpan(6, 0, 0)), firstInsolation.Time);

                Assert.AreEqual(30, lastInsolation.Energy);
                Assert.AreEqual(123, lastInsolation.Power);
                Assert.AreEqual(DateTime.Today.Add(new TimeSpan(6, 35, 0)), lastInsolation.Time);
            });
        }
    }
}
