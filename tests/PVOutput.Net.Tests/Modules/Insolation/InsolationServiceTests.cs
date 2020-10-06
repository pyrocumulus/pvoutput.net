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
using PVOutput.Net.Requests.Modules;

namespace PVOutput.Net.Tests.Modules.Insolation
{
    [TestFixture]
    public partial class InsolationServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task InsolationService_GetForOwnSystem_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETINSOLATION_URL)
                        .RespondPlainText(INSOLATION_RESPONSE_BASIC);

            var response = await client.Insolation.GetInsolationForOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task InsolationService_GetForSystem_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETINSOLATION_URL)
                        .WithQueryString("sid1=54321")
                        .RespondPlainText(INSOLATION_RESPONSE_BASIC);

            var response = await client.Insolation.GetInsolationForSystemAsync(54321);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task InsolationService_GetForLocation_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETINSOLATION_URL)
                        .WithQueryString("ll=-84.623970,42.728820")
                        .RespondPlainText(INSOLATION_RESPONSE_BASIC);

            var response = await client.Insolation.GetInsolationForLocationAsync(new PVCoordinate(-84.62397m, 42.72882m));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void InsolationRequest_Date_CreatesCorrectUriParameters()
        {
            var request = new InsolationRequest() { Date = new DateTime(2018, 6, 12) };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["d"], Is.EqualTo("20180612"));
        }

        [Test]
        public void InsolationRequest_SystemId_CreatesCorrectUriParameters()
        {
            var request = new InsolationRequest() { SystemId = 1234 };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["sid1"], Is.EqualTo(1234));
        }

        [Test]
        public void InsolationRequest_Coordinate_CreatesCorrectUriParameters()
        {
            var request = new InsolationRequest() { Coordinate = new PVCoordinate(-84.62397m, 42.72882m) };
            var parameters = request.GetUriPathParameters();
            Assert.That(parameters["ll"], Is.EqualTo("-84.623970,42.728820"));
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
                Assert.That(firstInsolation.Energy, Is.EqualTo(0));
                Assert.That(firstInsolation.Power, Is.EqualTo(0));
                Assert.That(firstInsolation.Time, Is.EqualTo(new TimeSpan(6, 0, 0)));

                Assert.That(lastInsolation.Energy, Is.EqualTo(30));
                Assert.That(lastInsolation.Power, Is.EqualTo(123));
                Assert.That(lastInsolation.Time, Is.EqualTo(new TimeSpan(6, 35, 0)));
            });
        }
    }
}
