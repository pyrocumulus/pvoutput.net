using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Extended
{
    [TestFixture]
    public class ExtendedServiceTests
    {
        [Test]
        public async Task ExtendedService_GetRecent()
        {
            var client = TestUtility.GetMockClient(ExtendedTestsData.GETEXTENDED_URL, ExtendedTestsData.EXTENDED_RESPONSE_RECENT);
            var response = await client.Extended.GetRecentExtendedDataAsync();

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var extended = response.Value;
            Assert.AreEqual(new DateTime(2014, 3, 7), extended.Date);

            Assert.AreEqual(67.4d, extended.ExtendedValue1);
            Assert.AreEqual(825.321d, extended.ExtendedValue2);
            Assert.AreEqual(349.0d, extended.ExtendedValue3);
            Assert.AreEqual(1105.0d, extended.ExtendedValue4);
            Assert.AreEqual(1115.0d, extended.ExtendedValue5);
            Assert.AreEqual(-12.3d, extended.ExtendedValue6);
        }

        [Test]
        public async Task ExtendedService_GetPeriod()
        {
            var client = TestUtility.GetMockClient(ExtendedTestsData.GETEXTENDED_URL, ExtendedTestsData.EXTENDED_RESPONSE_PERIOD);
            var response = await client.Extended.GetExtendedDataForPeriodAsync(new DateTime(2014 , 3, 7), new DateTime(2014, 3, 8));

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);
            Assert.AreEqual(2, response.Value.Count());

            var extended1 = response.Value.First();
            Assert.AreEqual(new DateTime(2014, 3, 7), extended1.Date);

            Assert.AreEqual(67.4d, extended1.ExtendedValue1);
            Assert.AreEqual(825.321d, extended1.ExtendedValue2);
            Assert.AreEqual(349.0d, extended1.ExtendedValue3);
            Assert.AreEqual(1105.0d, extended1.ExtendedValue4);
            Assert.AreEqual(1115.0d, extended1.ExtendedValue5);
            Assert.AreEqual(-12.3d, extended1.ExtendedValue6);

            var extended2 = response.Value.Last();
            Assert.AreEqual(new DateTime(2014, 3, 8), extended2.Date);
            Assert.AreEqual(68.2d, extended2.ExtendedValue1);
            Assert.AreEqual(846.254d, extended2.ExtendedValue2);
            Assert.AreEqual(323.0d, extended2.ExtendedValue3);
            Assert.AreEqual(1206.0d, extended2.ExtendedValue4);
            Assert.AreEqual(1123.0d, extended2.ExtendedValue5);
            Assert.AreEqual(-15.2d, extended2.ExtendedValue6);
        }
    }
}
