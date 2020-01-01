using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public partial class StatusServiceTests
    {
        [Test]
        public async Task StatusService_GetStatusForDateTime_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);
            
            testProvider.ExpectUriFromBase(GETSTATUS_URL)
                        .WithQueryString("d=20190131&t=14:00")
                        .RespondPlainText(STATUS_RESPONSE_SINGLE);

            var response = await client.Status.GetStatusForDateTimeAsync(new DateTime(2019, 1, 31, 14, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);

            var status = response.Value;
            Assert.IsNotNull(status);
        }

        [Test]
        public async Task StatusService_GetHistoryForPeriod_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETSTATUS_URL)
                        .WithQueryString("d=20190131&from=14:00&to=15:00&h=1")
                        .RespondPlainText(STATUS_RESPONSE_HISTORY);

            var response = await client.Status.GetHistoryForPeriodAsync(new DateTime(2019, 1, 31, 14, 0, 0), new DateTime(2019, 1, 31, 15, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();

            var statusList = response.Values;
            Assert.Multiple(() =>
            {
                Assert.IsNull(response.Exception);
                Assert.IsTrue(response.HasValues);
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual(10, statusList.Count());
                Assert.IsNotNull(statusList.First());
            });
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task StatusReader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IStatus>();
            IStatus result = await reader.ReadObjectAsync(new StringReader(STATUS_RESPONSE_SINGLE));

            Assert.Multiple(() =>
            {

                Assert.AreEqual(new DateTime(2019, 1, 31, 14, 0, 0), result.Timestamp);
                Assert.AreEqual(2930, result.EnergyGeneration);
                Assert.AreEqual(459, result.PowerGeneration);
                Assert.AreEqual(5938, result.EnergyConsumption);
                Assert.AreEqual(386, result.PowerConsumption);
                Assert.AreEqual(0.111d, result.NormalisedOutput);
                Assert.AreEqual(1.8d, result.Temperature);
                Assert.AreEqual(230.1d, result.Voltage);
                Assert.AreEqual(1d, result.ExtendedValue1);
                Assert.AreEqual(2d, result.ExtendedValue2);
                Assert.AreEqual(3d, result.ExtendedValue3);
                Assert.AreEqual(4d, result.ExtendedValue4);
                Assert.AreEqual(5d, result.ExtendedValue5);
                Assert.AreEqual(6d, result.ExtendedValue6);
            });
        }


        [Test]
        public async Task StatusHistoryReader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IStatusHistory>();
            IStatusHistory result = await reader.ReadObjectAsync(new StringReader(STATUS_RESPONSE_HISTORY_SINGLE));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2930, result.EnergyGeneration);
                Assert.AreEqual(0.710d, result.EnergyEfficiency);
                Assert.AreEqual(459, result.InstantaneousPower);
                Assert.AreEqual(456, result.AveragePower);
                Assert.AreEqual(0.111d, result.NormalisedOutput);
                Assert.AreEqual(5938, result.EnergyConsumption);
                Assert.AreEqual(386, result.PowerConsumption);
                Assert.AreEqual(1.8d, result.Temperature);
                Assert.AreEqual(230.1d, result.Volts);
                Assert.AreEqual(1d, result.ExtendedValue1);
                Assert.AreEqual(2d, result.ExtendedValue2);
                Assert.AreEqual(3d, result.ExtendedValue3);
                Assert.AreEqual(4d, result.ExtendedValue4);
                Assert.AreEqual(5d, result.ExtendedValue5);
                Assert.AreEqual(6d, result.ExtendedValue6);
            });
        }
    }
}
