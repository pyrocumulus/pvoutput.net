using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;
using PVOutput.Net.Builders;
using System.Collections;

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public partial class StatusServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task StatusService_GetStatusForDateTime_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            
            testProvider.ExpectUriFromBase(GETSTATUS_URL)
                        .WithQueryString("d=20190131&t=14:00")
                        .RespondPlainText(STATUS_RESPONSE_SINGLE);

            var response = await client.Status.GetStatusForDateTimeAsync(new DateTime(2019, 1, 31, 14, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task StatusService_GetHistoryForPeriod_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETSTATUS_URL)
                        .WithQueryString("d=20190131&from=14:00&to=15:00&h=1")
                        .RespondPlainText(STATUS_RESPONSE_HISTORY);

            var response = await client.Status.GetHistoryForPeriodAsync(new DateTime(2019, 1, 31, 14, 0, 0), new DateTime(2019, 1, 31, 15, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task StatusService_GetDayStatistics_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETSTATUS_URL)
                        .WithQueryString("d=20200131&from=10:00&to=16:00&stats=1")
                        .RespondPlainText(STATUS_RESPONSE_DAYSTATISTICS_MEDIUM);

            var response = await client.Status.GetDayStatisticsForPeriodAsync(new DateTime(2020, 1, 31, 10, 0, 0), new DateTime(2020, 1, 31, 16, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(new TimeSpan(14, 40, 0), response.Value.PeakTime);
                Assert.AreEqual(new TimeSpan(15, 5, 0), response.Value.StandbyPowerTime);
            });
        }

        [Test]
        public void StatusService_GetStatusForDateTime_WithFutureDate_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.GetStatusForDateTimeAsync(DateTime.Today.AddDays(1));
            });
        }

        [Test]
        public void StatusService_DeleteStatus_WithFutureDate_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.DeleteStatusAsync(DateTime.Today.AddDays(1));
            });
        }


        [Test]
        public void StatusService_GetHistoryForPeriod_WithFutureRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.GetHistoryForPeriodAsync(DateTime.Today, DateTime.Today.AddDays(1));
            });
        }

        [Test]
        public void StatusService_GetHistoryForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.GetHistoryForPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29));
            });
        }

        [Test]
        public void StatusService_GetDayStatisticsForPeriod_WithFutureRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.GetDayStatisticsForPeriodAsync(DateTime.Today, DateTime.Today.AddDays(1));
            });
        }

        [Test]
        public void StatusService_GetDayStatisticsForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.GetDayStatisticsForPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29));
            });
        }


        [Test]
        public void StatusService_AddStatus_WithNullStatus_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                _ = await client.Status.AddStatusAsync(null);
            });
        }

        [Test]
        public async Task StatusService_AddStatus_CallsCorrectUri()
        {
            var status = new StatusPostBuilder<IStatusPost>().SetTimeStamp(new DateTime(2020, 1, 1, 12, 22, 0))
                    .SetGeneration(11000).SetConsumption(9000).Build();

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(ADDSTATUS_URL)
                        .WithQueryString("d=20200101&t=12:22&v1=11000&v3=9000&n=0")
                        .RespondPlainText("");

            await client.Status.AddStatusAsync(status);
            testProvider.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void StatusService_AddBatchStatus_WithNullStatuses_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                _ = await client.Status.AddBatchStatusAsync(null);
            });
        }

        [Test]
        public void StatusService_AddBatchStatus_WithEmptyStatuses_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _ = await client.Status.AddBatchStatusAsync(new List<IBatchStatusPost>());
            });
        }

        [Test]
        public async Task StatusService_AddBatchStatus_CallsCorrectUri()
        {
            var batchStatus = new StatusPostBuilder<IBatchStatusPost>().SetTimeStamp(new DateTime(2020, 1, 1, 12, 22, 0))
                    .SetGeneration(11000).SetConsumption(9000).Build();

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(ADDBATCHSTATUS_URL)
                        .WithQueryString("n=0&data=20200101,12:22,11000,,9000,,,,,,,,,;")
                        .RespondPlainText("");

            await client.Status.AddBatchStatusAsync(new[] { batchStatus });
            testProvider.VerifyNoOutstandingExpectation();
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task StatusReader_ForResponse_CreatesCorrectObject()
        {
            IStatus result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatus>(STATUS_RESPONSE_SINGLE);

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
            IStatusHistory result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatusHistory>(STATUS_RESPONSE_HISTORY_SINGLE);

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

        [Test]
        public async Task StatusHistoryReader_ForPeriodResponse_CreatesCorrectObjects()
        {
            IEnumerable<IStatusHistory> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IStatusHistory>(STATUS_RESPONSE_HISTORY);

            var firstStatus = result.First();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, result.Count());

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
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForSmallResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_SMALL);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(334, result.EnergyGeneration);
                Assert.AreEqual(1, result.PowerGeneration);
                Assert.AreEqual(191, result.PeakPower);
                Assert.AreEqual(new TimeSpan(11, 0, 0), result.PeakTime);
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForMediumResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_MEDIUM);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(334, result.EnergyGeneration);
                Assert.AreEqual(2, result.PowerGeneration);
                Assert.AreEqual(82, result.PeakPower);
                Assert.AreEqual(new TimeSpan(14, 40, 0), result.PeakTime);

                Assert.AreEqual(5811, result.EnergyConsumption);
                Assert.AreEqual(417, result.PowerConsumption);
                Assert.AreEqual(255, result.StandbyPower);
                Assert.AreEqual(new TimeSpan(15, 5, 0), result.StandbyPowerTime);
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForFullResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_FULL);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(35302, result.EnergyGeneration);
                Assert.AreEqual(3, result.PowerGeneration);
                Assert.AreEqual(5369, result.PeakPower);
                Assert.AreEqual(new TimeSpan(12, 45, 0), result.PeakTime);

                Assert.AreEqual(31476, result.EnergyConsumption);
                Assert.AreEqual(606, result.PowerConsumption);
                Assert.AreEqual(495, result.StandbyPower);
                Assert.AreEqual(new TimeSpan(9, 35, 0), result.StandbyPowerTime);

                Assert.AreEqual(18.1d, result.MinimumTemperature);
                Assert.AreEqual(26.6d, result.MaximumTemperature);
                Assert.AreEqual(21.7d, result.AverageTemperature);
            });
        }
    }
}
