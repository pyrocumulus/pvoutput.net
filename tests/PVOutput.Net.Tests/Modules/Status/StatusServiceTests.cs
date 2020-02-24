﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

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

            var response = await client.Status.GetDayStatisticsForPeriodAsync(new DateTime(2020, 1, 31, 10, 0, 0), new DateTime(2019, 1, 31, 16, 0, 0));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);

            Assert.AreEqual(new DateTime(2020, 1, 31, 14, 40, 0), response.Value.PeakTime);
            Assert.AreEqual(new DateTime(2020, 1, 31, 15, 5, 0), response.Value.StandbyPowerTime);
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
                Assert.AreEqual(DateTime.Today.AddHours(11), result.PeakTime);
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
                Assert.AreEqual(DateTime.Today.AddHours(14).AddMinutes(40), result.PeakTime);

                Assert.AreEqual(5811, result.EnergyConsumption);
                Assert.AreEqual(417, result.PowerConsumption);
                Assert.AreEqual(255, result.StandbyPower);
                Assert.AreEqual(DateTime.Today.AddHours(15).AddMinutes(5), result.StandbyPowerTime);
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
                Assert.AreEqual(DateTime.Today.AddHours(12).AddMinutes(45), result.PeakTime);

                Assert.AreEqual(31476, result.EnergyConsumption);
                Assert.AreEqual(606, result.PowerConsumption);
                Assert.AreEqual(495, result.StandbyPower);
                Assert.AreEqual(DateTime.Today.AddHours(9).AddMinutes(35), result.StandbyPowerTime);

                Assert.AreEqual(18.1d, result.MinimumTemperature);
                Assert.AreEqual(26.6d, result.MaximumTemperature);
                Assert.AreEqual(21.7d, result.AverageTemperature);
            });
        }
    }
}
