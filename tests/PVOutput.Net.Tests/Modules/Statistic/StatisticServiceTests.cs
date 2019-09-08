using NUnit.Framework;
using PVOutput.Net.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Modules.Statistic
{
	[TestFixture]
	public class StatisticServiceTests
	{
		[Test]
		public async Task StatisticsService_GetLifetimeStatistics()
		{
			var client = TestUtility.GetMockClient(StatisticTestsData.GETSYSTEM_URL, StatisticTestsData.STATISTIC_RESPONSE_SIMPLE);
			var response = await client.Statistics.GetLifetimeStatisticsAsync();

			if (response.Exception != null)
				throw response.Exception;

			Assert.IsTrue(response.HasValue);
			Assert.IsNotNull(response.IsSuccess);

			var statistic = response.Value;
			Assert.AreEqual(10052033, statistic.EnergyGenerated);
			Assert.AreEqual(4366048, statistic.EnergyExported);
			Assert.AreEqual(9031, statistic.AverageGeneration);
			Assert.AreEqual(10, statistic.MinimumGeneration);
			Assert.AreEqual(25473, statistic.MaximumGeneration);
			Assert.AreEqual(2.189d, statistic.AverageEfficiency);
			Assert.AreEqual(1001, statistic.Outputs);
			Assert.AreEqual(new DateTime(2016, 1, 1), statistic.ActualDateFrom);
			Assert.AreEqual(new DateTime(2019, 9, 8), statistic.ActualDateTo);
			Assert.AreEqual(6.175d, statistic.RecordEfficiency);
			Assert.AreEqual(new DateTime(2018, 6, 1), statistic.RecordDate);
		}

		[Test]
		public async Task StatisticsService_GetPeriodStatistics()
		{
			var client = TestUtility.GetMockClient(StatisticTestsData.GETSYSTEM_URL, StatisticTestsData.STATISTIC_RESPONSE_ALL);
			var response = await client.Statistics.GetStatisticsForPeriodAsync(new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));

			if (response.Exception != null)
				throw response.Exception;

			Assert.IsTrue(response.HasValue);
			Assert.IsNotNull(response.IsSuccess);

			var statistic = response.Value;
			Assert.AreEqual(10052033, statistic.EnergyGenerated);
			Assert.AreEqual(4366048, statistic.EnergyExported);
			Assert.AreEqual(9031, statistic.AverageGeneration);
			Assert.AreEqual(10, statistic.MinimumGeneration);
			Assert.AreEqual(25473, statistic.MaximumGeneration);
			Assert.AreEqual(2.189d, statistic.AverageEfficiency);
			Assert.AreEqual(1001, statistic.Outputs);
			Assert.AreEqual(new DateTime(2016, 1, 1), statistic.ActualDateFrom);
			Assert.AreEqual(new DateTime(2019, 9, 8), statistic.ActualDateTo);
			Assert.AreEqual(6.175d, statistic.RecordEfficiency);
			Assert.AreEqual(new DateTime(2018, 6, 1), statistic.RecordDate);

			// 7667632,5675645,0,0,0,10969,697,30851
			Assert.AreEqual(7667632, statistic.EnergyConsumed);
			Assert.AreEqual(5675645, statistic.PeakEnergyImport);
			Assert.AreEqual(22, statistic.OffPeakEnergyImport);
			Assert.AreEqual(23, statistic.ShoulderEnergyImport);
			Assert.AreEqual(24, statistic.HighShoulderEnergyImport);
			Assert.AreEqual(10969, statistic.AverageConsumption);
			Assert.AreEqual(697, statistic.MinimumConsumption);
			Assert.AreEqual(30851, statistic.MaximumConsumption);
		}
	}
}
