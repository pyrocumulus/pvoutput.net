﻿using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;
using PVOutput.Net.Requests.Modules;
using System.Collections.Generic;

namespace PVOutput.Net.Tests.Modules.Statistic
{
    [TestFixture]
    public partial class StatisticServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task StatisticsService_GetLifetimeStatistics_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETSTATISTIC_URL)
                        .RespondPlainText(STATISTIC_RESPONSE_SIMPLE);

            Responses.PVOutputResponse<IStatistic> response = await client.Statistics.GetLifetimeStatisticsAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task StatisticsService_GetPeriodStatistics_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETSTATISTIC_URL)
                        .WithQueryString("df=20170101&dt=20180101")
                        .RespondPlainText(STATISTIC_RESPONSE_ALL);

            Responses.PVOutputResponse<IStatistic> response 
                = await client.Statistics.GetStatisticsForPeriodAsync(new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void StatisticsService_GetStatisticsForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Statistics.GetStatisticsForPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29));
            });
        }

        [Test]
        public void StatisticsRequest_SystemId_CreatesCorrectUriParameters()
        {
            var request = new StatisticRequest() { SystemId = 1234 };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["sid1"], Is.EqualTo(1234));
        }

        [Test]
        public void StatisticsRequest_IncludeConsumptionImport_CreatesCorrectUriParameters()
        {
            var request = new StatisticRequest() { IncludeConsumptionImport = true };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["c"], Is.EqualTo(1));
        }

        [Test]
        public void StatisticsRequest_IncludeCreditDebit_CreatesCorrectUriParameters()
        {
            var request = new StatisticRequest() { IncludeCreditDebit = true };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["cdr"], Is.EqualTo(1));
        }

        [Test]
        public void StatisticsPeriodRequest_SystemId_CreatesCorrectUriParameters()
        {
            var request = new StatisticPeriodRequest() { SystemId = 1234 };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["sid1"], Is.EqualTo(1234));
        }

        [Test]
        public void StatisticsPeriodRequest_IncludeConsumptionImport_CreatesCorrectUriParameters()
        {
            var request = new StatisticPeriodRequest() { IncludeConsumptionImport = true };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["c"], Is.EqualTo(1));
        }

        [Test]
        public void StatisticsPeriodRequest_IncludeCreditDebit_CreatesCorrectUriParameters()
        {
            var request = new StatisticPeriodRequest() { IncludeCreditDebit = true };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["cdr"], Is.EqualTo(1));
        }

        [Test]
        public void StatisticsPeriodRequest_FromDate_CreatesCorrectUriParameters()
        {
            var request = new StatisticPeriodRequest() { FromDate = new DateTime(2018, 6, 12) };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["df"], Is.EqualTo("20180612"));
        }

        [Test]
        public void StatisticsPeriodRequest_ToDate_CreatesCorrectUriParameters()
        {
            var request = new StatisticPeriodRequest() { ToDate = new DateTime(2018, 6, 12) };
            IDictionary<string, object> parameters = request.GetUriPathParameters();
            Assert.That(parameters["dt"], Is.EqualTo("20180612"));
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task StatisticsReader_ForResponse_CreatesCorrectObject()
        {
            IStatistic result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatistic>(STATISTIC_RESPONSE_SIMPLE);

            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGenerated, Is.EqualTo(10052033));
                Assert.That(result.EnergyExported, Is.EqualTo(4366048));
                Assert.That(result.AverageGeneration, Is.EqualTo(9031));
                Assert.That(result.MinimumGeneration, Is.EqualTo(10));
                Assert.That(result.MaximumGeneration, Is.EqualTo(25473));
                Assert.That(result.AverageEfficiency, Is.EqualTo(2.189d));
                Assert.That(result.Outputs, Is.EqualTo(1001));
                Assert.That(result.ActualDateFrom, Is.EqualTo(new DateTime(2016, 1, 1)));
                Assert.That(result.ActualDateTo, Is.EqualTo(new DateTime(2019, 9, 8)));
                Assert.That(result.RecordEfficiency, Is.EqualTo(6.175d));
                Assert.That(result.RecordDate, Is.EqualTo(new DateTime(2018, 6, 1)));
            });
        }


        [Test]
        public async Task StatisticsReader_ForPeriodResponse_CreatesCorrectObject()
        {
            IStatistic result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatistic>(STATISTIC_RESPONSE_ALL);
            
            Assert.Multiple(() =>
            {
                Assert.That(result.EnergyGenerated, Is.EqualTo(10052033));
                Assert.That(result.EnergyExported, Is.EqualTo(4366048));
                Assert.That(result.AverageGeneration, Is.EqualTo(9031));
                Assert.That(result.MinimumGeneration, Is.EqualTo(10));
                Assert.That(result.MaximumGeneration, Is.EqualTo(25473));
                Assert.That(result.AverageEfficiency, Is.EqualTo(2.189d));
                Assert.That(result.Outputs, Is.EqualTo(1001));
                Assert.That(result.ActualDateFrom, Is.EqualTo(new DateTime(2016, 1, 1)));
                Assert.That(result.ActualDateTo, Is.EqualTo(new DateTime(2019, 9, 8)));
                Assert.That(result.RecordEfficiency, Is.EqualTo(6.175d));
                Assert.That(result.RecordDate, Is.EqualTo(new DateTime(2018, 6, 1)));
                Assert.That(result.EnergyConsumed, Is.EqualTo(7667632));
                Assert.That(result.PeakEnergyImport, Is.EqualTo(5675645));
                Assert.That(result.OffPeakEnergyImport, Is.EqualTo(22));
                Assert.That(result.ShoulderEnergyImport, Is.EqualTo(23));
                Assert.That(result.HighShoulderEnergyImport, Is.EqualTo(24));
                Assert.That(result.AverageConsumption, Is.EqualTo(10969));
                Assert.That(result.MinimumConsumption, Is.EqualTo(697));
                Assert.That(result.MaximumConsumption, Is.EqualTo(30851));
                Assert.That(result.CreditAmount, Is.EqualTo(37.29d));
                Assert.That(result.DebitAmount, Is.EqualTo(40.81d));
            });
        }
    }
}
