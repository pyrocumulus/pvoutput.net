using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public class StatusServiceTests
    {
        [Test]
        public async Task StatusService_GetStatusForDateTime()
        {
            var client = TestUtility.GetMockClient(StatusServiceTestsData.GETSTATUS_URL, StatusServiceTestsData.STATUS_RESPONSE_SINGLE);
            var response = await client.Status.GetStatusForDateTimeAsync(new DateTime(2019, 1, 31, 14, 0, 0));

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var status = response.Value;
            Assert.AreEqual(new DateTime(2019, 1, 31, 14, 0, 0), status.Timestamp);
            Assert.AreEqual(2930, status.EnergyGeneration);
            Assert.AreEqual(459, status.PowerGeneration);
            Assert.AreEqual(5938, status.EnergyConsumption);
            Assert.AreEqual(386, status.PowerConsumption);
            Assert.AreEqual(0.111d, status.NormalisedOutput);
            Assert.AreEqual(1.8d, status.Temperature);
            Assert.AreEqual(230.1d, status.Voltage);
            Assert.AreEqual(1d, status.ExtendedValue1);
            Assert.AreEqual(2d, status.ExtendedValue2);
            Assert.AreEqual(3d, status.ExtendedValue3);
            Assert.AreEqual(4d, status.ExtendedValue4);
            Assert.AreEqual(5d, status.ExtendedValue5);
            Assert.AreEqual(6d, status.ExtendedValue6);
        }

        [Test]
        public async Task StatusService_GetHistoryForPeriod()
        {
            var client = TestUtility.GetMockClient(StatusServiceTestsData.GETSTATUS_URL, StatusServiceTestsData.STATUS_RESPONSE_HISTORY);
            var response = await client.Status.GetHistoryForPeriodAsync(new DateTime(2019, 1, 31, 14, 0, 0), new DateTime(2019, 1, 31, 15, 0, 0));

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.IsSuccess);

            var statusList = response.Values;

            Assert.AreEqual(10, statusList.Count());

            var firstStatus = statusList.First();
            Assert.AreEqual(2930, firstStatus.EnergyGeneration);
            Assert.AreEqual(0.710d, firstStatus.EnergyEfficiency);
            Assert.AreEqual(459, firstStatus.InstantaneousPower);
            Assert.AreEqual(456, firstStatus.AveragePower);
            Assert.AreEqual(0.111d, firstStatus.NormalisedOutput);
            Assert.AreEqual(5938, firstStatus.EnergyConsumption);
            Assert.AreEqual(386, firstStatus.PowerConsumption);
            Assert.AreEqual(1.8d, firstStatus.Temperature);
            Assert.AreEqual(230.1d, firstStatus.Volts);
            Assert.AreEqual(1d, firstStatus.ExtendedValue1);
            Assert.AreEqual(2d, firstStatus.ExtendedValue2);
            Assert.AreEqual(3d, firstStatus.ExtendedValue3);
            Assert.AreEqual(4d, firstStatus.ExtendedValue4);
            Assert.AreEqual(5d, firstStatus.ExtendedValue5);
            Assert.AreEqual(6d, firstStatus.ExtendedValue6);
        }
    }
}
