using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

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

            var response = await client.Statistics.GetLifetimeStatisticsAsync();
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

            var response = await client.Statistics.GetStatisticsForPeriodAsync(new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));
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

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task StatisticsReader_ForResponse_CreatesCorrectObject()
        {
            var result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatistic>(STATISTIC_RESPONSE_SIMPLE);

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
            var result = await TestUtility.ExecuteObjectReaderByTypeAsync<IStatistic>(STATISTIC_RESPONSE_ALL);

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
        }
    }
}
