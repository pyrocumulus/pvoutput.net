using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Extended
{
    [TestFixture]
    public partial class ExtendedServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task ExtendedService_GetRecent_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETEXTENDED_URL)
                        .RespondPlainText(EXTENDED_RESPONSE_RECENT);

            var response = await client.Extended.GetRecentExtendedDataAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task ExtendedService_GetPeriod_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETEXTENDED_URL)
                        .WithQueryString("df=20140307&dt=20140308")
                        .RespondPlainText(EXTENDED_RESPONSE_PERIOD);

            var response = await client.Extended.GetExtendedDataForPeriodAsync(new DateTime(2014, 3, 7), new DateTime(2014, 3, 8));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task ExtendedService_WithPeriodAndLimit_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETEXTENDED_URL)
                        .WithQueryString("df=20160307&dt=20160308&limit=10")
                        .RespondPlainText(EXTENDED_RESPONSE_PERIOD);

            var response = await client.Extended.GetExtendedDataForPeriodAsync(new DateTime(2016, 3, 7), new DateTime(2016, 3, 8), 10);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void ExtendedService_GetExtendedDataForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Extended.GetExtendedDataForPeriodAsync(new DateTime(2016, 3, 7), new DateTime(2016, 3, 6));
            });
        }

        [Test]
        public void ExtendedService_GetExtendedDataForPeriod_WithLimitThatIsTooHigh_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Extended.GetExtendedDataForPeriodAsync(DateTime.Today.AddDays(-1), DateTime.Today, 100);
            });
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task ExtendedReader_ForRecentResponse_CreatesCorrectObject()
        {
            IExtended result = await TestUtility.ExecuteObjectReaderByTypeAsync<IExtended>(EXTENDED_RESPONSE_RECENT);

            Assert.Multiple(() =>
            {
                Assert.That(result.ExtendedValue1, Is.EqualTo(67.4d));
                Assert.That(result.ExtendedValue2, Is.EqualTo(825.321d));
                Assert.That(result.ExtendedValue3, Is.EqualTo(349.0d));
                Assert.That(result.ExtendedValue4, Is.EqualTo(1105.0d));
                Assert.That(result.ExtendedValue5, Is.EqualTo(1115.0d));
                Assert.That(result.ExtendedValue6, Is.EqualTo(-12.3d));
            });
        }

        [Test]
        public async Task ExtendedReader_ForPeriodResponse_CreatesCorrectObjects()
        {
            IEnumerable<IExtended> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IExtended>(EXTENDED_RESPONSE_PERIOD);

            var extended1 = result.First();
            var extended2 = result.Last();

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(2).Items);
                Assert.That(extended1.ExtendedDate, Is.EqualTo(new DateTime(2014, 3, 7)));

                Assert.That(extended1.ExtendedValue1, Is.EqualTo(67.4d));
                Assert.That(extended1.ExtendedValue2, Is.EqualTo(825.321d));
                Assert.That(extended1.ExtendedValue3, Is.EqualTo(349.0d));
                Assert.That(extended1.ExtendedValue4, Is.EqualTo(1105.0d));
                Assert.That(extended1.ExtendedValue5, Is.EqualTo(1115.0d));
                Assert.That(extended1.ExtendedValue6, Is.EqualTo(-12.3d));

                Assert.That(extended2.ExtendedDate, Is.EqualTo(new DateTime(2014, 3, 8)));
                Assert.That(extended2.ExtendedValue1, Is.EqualTo(68.2d));
                Assert.That(extended2.ExtendedValue2, Is.EqualTo(846.254d));
                Assert.That(extended2.ExtendedValue3, Is.EqualTo(323.0d));
                Assert.That(extended2.ExtendedValue4, Is.EqualTo(1206.0d));
                Assert.That(extended2.ExtendedValue5, Is.EqualTo(1123.0d));
                Assert.That(extended2.ExtendedValue6, Is.EqualTo(-15.2d));
            });
        }
    }
}
