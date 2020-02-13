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
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETEXTENDED_URL)
                        .RespondPlainText(EXTENDED_RESPONSE_RECENT);

            var response = await client.Extended.GetRecentExtendedDataAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task ExtendedService_GetPeriod_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

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
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETEXTENDED_URL)
                        .WithQueryString("df=20160307&dt=20160308&limit=10")
                        .RespondPlainText(EXTENDED_RESPONSE_PERIOD);

            var response = await client.Extended.GetExtendedDataForPeriodAsync(new DateTime(2016, 3, 7), new DateTime(2016, 3, 8), 10);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
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
                Assert.AreEqual(67.4d, result.ExtendedValue1);
                Assert.AreEqual(825.321d, result.ExtendedValue2);
                Assert.AreEqual(349.0d, result.ExtendedValue3);
                Assert.AreEqual(1105.0d, result.ExtendedValue4);
                Assert.AreEqual(1115.0d, result.ExtendedValue5);
                Assert.AreEqual(-12.3d, result.ExtendedValue6);
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
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(new DateTime(2014, 3, 7), extended1.Date);

                Assert.AreEqual(67.4d, extended1.ExtendedValue1);
                Assert.AreEqual(825.321d, extended1.ExtendedValue2);
                Assert.AreEqual(349.0d, extended1.ExtendedValue3);
                Assert.AreEqual(1105.0d, extended1.ExtendedValue4);
                Assert.AreEqual(1115.0d, extended1.ExtendedValue5);
                Assert.AreEqual(-12.3d, extended1.ExtendedValue6);

                Assert.AreEqual(new DateTime(2014, 3, 8), extended2.Date);
                Assert.AreEqual(68.2d, extended2.ExtendedValue1);
                Assert.AreEqual(846.254d, extended2.ExtendedValue2);
                Assert.AreEqual(323.0d, extended2.ExtendedValue3);
                Assert.AreEqual(1206.0d, extended2.ExtendedValue4);
                Assert.AreEqual(1123.0d, extended2.ExtendedValue5);
                Assert.AreEqual(-15.2d, extended2.ExtendedValue6);
            });
        }
    }
}
