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
                Assert.That(response.Value.PeakTime, Is.EqualTo(new TimeSpan(14, 40, 0)));
                Assert.That(response.Value.StandbyPowerTime, Is.EqualTo(new TimeSpan(15, 5, 0)));
            });
        }


        [Test]
        public async Task StatusService_DeleteStatus_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            // Yesterday, 11:15
            var testDateTime = DateTime.Today.AddDays(-1).Add(new TimeSpan(11, 15, 0));
            string resultingDateString = testDateTime.ToString("yyyyMMdd");

            testProvider.ExpectUriFromBase(DELETESTATUS_URL)
                        .WithQueryString($"d={resultingDateString}&t=11:15")
                        .RespondPlainText("");

            var response = await client.Status.DeleteStatusAsync(testDateTime);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
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

        public static IEnumerable DeleteStatusWithDateOutsideBoundsTestCases
        {
            get
            {
                yield return new TestCaseData(DateTime.Today.AddDays(1));
                yield return new TestCaseData(DateTime.Today.AddDays(-2));
            }
        }

        [Test]
        [TestCaseSource(typeof(StatusServiceTests), nameof(DeleteStatusWithDateOutsideBoundsTestCases))]
        public void StatusService_DeleteStatus_WithDateOutsideBounds_Throws(DateTime testDate)
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Status.DeleteStatusAsync(testDate);
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
                Assert.That(result.Timestamp, Is.EqualTo(new DateTime(2019, 1, 31, 14, 0, 0)));
                Assert.That(result.EnergyGeneration, Is.EqualTo(2930));
                Assert.That(result.PowerGeneration, Is.EqualTo(459));
                Assert.That(result.EnergyConsumption, Is.EqualTo(5938));
                Assert.That(result.PowerConsumption, Is.EqualTo(386));
                Assert.That(result.NormalisedOutput, Is.EqualTo(0.111d));
                Assert.That(result.Temperature, Is.EqualTo(1.8d));
                Assert.That(result.Voltage, Is.EqualTo(230.1d));
                Assert.That(result.ExtendedValue1, Is.EqualTo(1d));
                Assert.That(result.ExtendedValue2, Is.EqualTo(2d));
                Assert.That(result.ExtendedValue3, Is.EqualTo(3d));
                Assert.That(result.ExtendedValue4, Is.EqualTo(4d));
                Assert.That(result.ExtendedValue5, Is.EqualTo(5d));
                Assert.That(result.ExtendedValue6, Is.EqualTo(6d));
            });
        }


        [Test]
        public async Task StatusHistoryReader_ForResponse_CreatesCorrectObject()
        {
            IStatusHistory result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatusHistory>(STATUS_RESPONSE_HISTORY_SINGLE);

            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGeneration, Is.EqualTo(2930));
                Assert.That(result.EnergyEfficiency, Is.EqualTo(0.710d));
                Assert.That(result.InstantaneousPower, Is.EqualTo(459));
                Assert.That(result.AveragePower, Is.EqualTo(456));
                Assert.That(result.NormalisedOutput, Is.EqualTo(0.111d));
                Assert.That(result.EnergyConsumption, Is.EqualTo(5938));
                Assert.That(result.PowerConsumption, Is.EqualTo(386));
                Assert.That(result.Temperature, Is.EqualTo(1.8d));
                Assert.That(result.Volts, Is.EqualTo(230.1d));
                Assert.That(result.ExtendedValue1, Is.EqualTo(1d));
                Assert.That(result.ExtendedValue2, Is.EqualTo(2d));
                Assert.That(result.ExtendedValue3, Is.EqualTo(3d));
                Assert.That(result.ExtendedValue4, Is.EqualTo(4d));
                Assert.That(result.ExtendedValue5, Is.EqualTo(5d));
                Assert.That(result.ExtendedValue6, Is.EqualTo(6d));
            });
        }

        [Test]
        public async Task StatusHistoryReader_ForPeriodResponse_CreatesCorrectObjects()
        {
            IEnumerable<IStatusHistory> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IStatusHistory>(STATUS_RESPONSE_HISTORY);

            var firstStatus = result.First();
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Exactly(10).Items);

                Assert.That(firstStatus.EnergyGeneration, Is.EqualTo(2930));
                Assert.That(firstStatus.EnergyEfficiency, Is.EqualTo(0.710d));
                Assert.That(firstStatus.InstantaneousPower, Is.EqualTo(459));
                Assert.That(firstStatus.AveragePower, Is.EqualTo(456));
                Assert.That(firstStatus.NormalisedOutput, Is.EqualTo(0.111d));
                Assert.That(firstStatus.EnergyConsumption, Is.EqualTo(5938));
                Assert.That(firstStatus.PowerConsumption, Is.EqualTo(386));
                Assert.That(firstStatus.Temperature, Is.EqualTo(1.8d));
                Assert.That(firstStatus.Volts, Is.EqualTo(230.1d));
                Assert.That(firstStatus.ExtendedValue1, Is.EqualTo(1d));
                Assert.That(firstStatus.ExtendedValue2, Is.EqualTo(2d));
                Assert.That(firstStatus.ExtendedValue3, Is.EqualTo(3d));
                Assert.That(firstStatus.ExtendedValue4, Is.EqualTo(4d));
                Assert.That(firstStatus.ExtendedValue5, Is.EqualTo(5d));
                Assert.That(firstStatus.ExtendedValue6, Is.EqualTo(6d));
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForSmallResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_SMALL);

            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGeneration, Is.EqualTo(334));
                Assert.That(result.PowerGeneration, Is.EqualTo(1));
                Assert.That(result.PeakPower, Is.EqualTo(191));
                Assert.That(result.PeakTime, Is.EqualTo(new TimeSpan(11, 0, 0)));
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForMediumResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_MEDIUM);

            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGeneration, Is.EqualTo(334));
                Assert.That(result.PowerGeneration, Is.EqualTo(2));
                Assert.That(result.PeakPower, Is.EqualTo(82));
                Assert.That(result.PeakTime, Is.EqualTo(new TimeSpan(14, 40, 0)));

                Assert.That(result.EnergyConsumption, Is.EqualTo(5811));
                Assert.That(result.PowerConsumption, Is.EqualTo(417));
                Assert.That(result.StandbyPower, Is.EqualTo(255));
                Assert.That(result.StandbyPowerTime, Is.EqualTo(new TimeSpan(15, 5, 0)));
            });
        }

        [Test]
        public async Task DayStatisticsReader_ForFullResponse_CreatesCorrectObjects()
        {
            IDayStatistics result = await TestUtility.ExecuteObjectReaderByTypeAsync<IDayStatistics>(STATUS_RESPONSE_DAYSTATISTICS_FULL);

            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGeneration, Is.EqualTo(35302));
                Assert.That(result.PowerGeneration, Is.EqualTo(3));
                Assert.That(result.PeakPower, Is.EqualTo(5369));
                Assert.That(result.PeakTime, Is.EqualTo(new TimeSpan(12, 45, 0)));

                Assert.That(result.EnergyConsumption, Is.EqualTo(31476));
                Assert.That(result.PowerConsumption, Is.EqualTo(606));
                Assert.That(result.StandbyPower, Is.EqualTo(495));
                Assert.That(result.StandbyPowerTime, Is.EqualTo(new TimeSpan(9, 35, 0)));

                Assert.That(result.MinimumTemperature, Is.EqualTo(18.1d));
                Assert.That(result.MaximumTemperature, Is.EqualTo(26.6d));
                Assert.That(result.AverageTemperature, Is.EqualTo(21.7d));
            });
        }
    }
}
