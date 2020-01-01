using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Statistic
{
    [TestFixture]
    public partial class StatisticServiceTests
    {
        [Test]
        public async Task StatisticsService_GetLifetimeStatistics_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETSTATISTIC_URL)
                        //.WithQueryString("d=20190131&t=14:00")
                        .RespondPlainText(STATISTIC_RESPONSE_SIMPLE);

            var response = await client.Statistics.GetLifetimeStatisticsAsync();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.IsNull(response.Exception);
            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Value);
        }

        [Test]
        public async Task StatisticsService_GetPeriodStatistics_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETSTATISTIC_URL)
                        .WithQueryString("df=20170101&dt=20180101")
                        .RespondPlainText(STATISTIC_RESPONSE_ALL);

            var response = await client.Statistics.GetStatisticsForPeriodAsync(new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));

            Assert.IsNull(response.Exception);
            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Value);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task StatisticsReader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IStatistic>();
            IStatistic result = await reader.ReadObjectAsync(new StringReader(STATISTIC_RESPONSE_SIMPLE));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(10052033, result.EnergyGenerated);
                Assert.AreEqual(4366048, result.EnergyExported);
                Assert.AreEqual(9031, result.AverageGeneration);
                Assert.AreEqual(10, result.MinimumGeneration);
                Assert.AreEqual(25473, result.MaximumGeneration);
                Assert.AreEqual(2.189d, result.AverageEfficiency);
                Assert.AreEqual(1001, result.Outputs);
                Assert.AreEqual(new DateTime(2016, 1, 1), result.ActualDateFrom);
                Assert.AreEqual(new DateTime(2019, 9, 8), result.ActualDateTo);
                Assert.AreEqual(6.175d, result.RecordEfficiency);
                Assert.AreEqual(new DateTime(2018, 6, 1), result.RecordDate);
            });
        }


        [Test]
        public async Task StatisticsReader_ForPeriodResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IStatistic>();
            IStatistic result = await reader.ReadObjectAsync(new StringReader(STATISTIC_RESPONSE_ALL));

            Assert.AreEqual(10052033, result.EnergyGenerated);
            Assert.AreEqual(4366048, result.EnergyExported);
            Assert.AreEqual(9031, result.AverageGeneration);
            Assert.AreEqual(10, result.MinimumGeneration);
            Assert.AreEqual(25473, result.MaximumGeneration);
            Assert.AreEqual(2.189d, result.AverageEfficiency);
            Assert.AreEqual(1001, result.Outputs);
            Assert.AreEqual(new DateTime(2016, 1, 1), result.ActualDateFrom);
            Assert.AreEqual(new DateTime(2019, 9, 8), result.ActualDateTo);
            Assert.AreEqual(6.175d, result.RecordEfficiency);
            Assert.AreEqual(new DateTime(2018, 6, 1), result.RecordDate);
            Assert.AreEqual(7667632, result.EnergyConsumed);
            Assert.AreEqual(5675645, result.PeakEnergyImport);
            Assert.AreEqual(22, result.OffPeakEnergyImport);
            Assert.AreEqual(23, result.ShoulderEnergyImport);
            Assert.AreEqual(24, result.HighShoulderEnergyImport);
            Assert.AreEqual(10969, result.AverageConsumption);
            Assert.AreEqual(697, result.MinimumConsumption);
            Assert.AreEqual(30851, result.MaximumConsumption);
        }
    }
}
