using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Output
{
    [TestFixture]
    public partial class OutputTests
    {
        //TODO: Test that MockClient receives a call for the right URI

        [Test]
        public async Task OutputService_WithBareData_GetDaily()
        {
            DateTime mockDate = new DateTime(2016, 10, 1);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, OUTPUT_RESPONSE_BARE);
            var response = await client.Output.GetOutputForDateAsync(mockDate, false);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);

            var output = response.Value;
            Assert.AreEqual(mockDate, output.Date);
        }


        [Test]
        public async Task OutputService_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, OUTPUT_RESPONSE_DAY);
            var response = await client.Output.GetOutputForDateAsync(mockDate, false);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var output = response.Value;
            Assert.AreEqual(mockDate, output.Date);
        }

        [Test]
        public async Task OutputService_GetWeek()
        {
            DateTime fromDate = new DateTime(2018, 9, 1);
            DateTime toDate = new DateTime(2018, 9, 7);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, OUTPUT_RESPONSE_WEEK);
            var response = await client.Output.GetOutputsForPeriodAsync(fromDate, toDate, false);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.Values);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Values.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }


        [Test]
        public async Task OutputService_WithInsolation_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, OUTPUT_WITH_INSOLATION_RESPONSE_DAY);
            var response = await client.Output.GetOutputForDateAsync(mockDate, true);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var output = response.Value;
            Assert.AreEqual(mockDate, output.Date);
            Assert.AreEqual(15197, output.Insolation);
        }

        [Test]
        public async Task OutputService_WithInsolation_GetWeek()
        {
            DateTime fromDate = new DateTime(2018, 9, 1);
            DateTime toDate = new DateTime(2018, 9, 7);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, OUTPUT_WITH_INSOLATION_RESPONSE_WEEK);
            var response = await client.Output.GetOutputsForPeriodAsync(fromDate, toDate, true);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.Values);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Values.OrderBy((o) => o.Date);

            Assert.AreEqual(15197, outputs.First().Insolation);
            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }

        [Test]
        public async Task OutputService_Team_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, TEAMOUTPUT_RESPONSE_DAY);
            var response = await client.Output.GetTeamOutputForDateAsync(mockDate, TestConstants.PVOUTPUT_TEAM_ID);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var output = response.Value;
            Assert.AreEqual(mockDate, output.Date);
        }

        [Test]
        public async Task OutputService_Team_GetWeek()
        {
            DateTime fromDate = new DateTime(2018, 9, 1);
            DateTime toDate = new DateTime(2018, 9, 7);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, TEAMOUTPUT_RESPONSE_WEEK);
            var response = await client.Output.GetTeamOutputsForPeriodAsync(fromDate, toDate, TestConstants.PVOUTPUT_TEAM_ID);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.Values);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Values.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }

        [Test]
        public async Task OutputService_Aggregated_GetByMonth()
        {
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 6, 30);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, AGGREGATEDOUTPUT_RESPONSE_MONTH);
            var response = await client.Output.GetAggregatedOutputsAsync(fromDate, toDate, AggregationPeriod.Month);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.Values);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Values.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate.Month, outputs.First().Date.Month);
            Assert.AreEqual(toDate.Month, outputs.Last().Date.Month);
            Assert.AreEqual(6, outputs.Count());
        }

        [Test]
        public async Task OutputService_Aggregated_GetByYear()
        {
            DateTime fromDate = new DateTime(2016, 1, 1);
            DateTime toDate = new DateTime(2018, 12, 31);

            var client = TestUtility.GetMockClient(GETOUTPUT_URL, AGGREGATEDOUPUT_RESPONSE_YEAR);
            var response = await client.Output.GetAggregatedOutputsAsync(fromDate, toDate, AggregationPeriod.Year);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsNotNull(response.Values);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Values.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate.Year, outputs.First().Date.Year);
            Assert.AreEqual(toDate.Year, outputs.Last().Date.Year);
            Assert.AreEqual(3, outputs.Count());
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task Outputreader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IOutput>();
            IOutput result = await reader.ReadObjectAsync(new StringReader(OUTPUT_RESPONSE_DAY));

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.Date);
                Assert.AreEqual(16784, result.EnergyGenerated);
                Assert.AreEqual(4.069, result.Efficiency);
                Assert.AreEqual(12719, result.EnergyExported);
                Assert.AreEqual(8500, result.EnergyUsed);
                Assert.AreEqual(3422, result.PeakPower);
                Assert.AreEqual(new DateTime(2018, 9, 1, 12, 0, 0), result.PeakTime);
                Assert.AreEqual("Fine", result.Condition);
                Assert.AreEqual(7, result.MinimumTemperature);
                Assert.AreEqual(23, result.MaximumTemperature);
                Assert.AreEqual(4435, result.PeakEnergyImport);
                Assert.AreEqual(123, result.OffPeakEnergyImport);
                Assert.AreEqual(321, result.ShoulderEnergyImport);
                Assert.AreEqual(456, result.HighShoulderEnergyImport);
            });
        }

        [Test]
        public async Task Outputreader_ForResponseWithInsolation_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IOutput>();
            IOutput result = await reader.ReadObjectAsync(new StringReader(OUTPUT_WITH_INSOLATION_RESPONSE_DAY));

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.Date);
                Assert.AreEqual(16784, result.EnergyGenerated);
                Assert.AreEqual(4.069, result.Efficiency);
                Assert.AreEqual(12719, result.EnergyExported);
                Assert.AreEqual(8500, result.EnergyUsed);
                Assert.AreEqual(3422, result.PeakPower);
                Assert.AreEqual(new DateTime(2018, 9, 1, 12, 0, 0), result.PeakTime);
                Assert.AreEqual("Fine", result.Condition);
                Assert.AreEqual(7, result.MinimumTemperature);
                Assert.AreEqual(23, result.MaximumTemperature);
                Assert.AreEqual(4435, result.PeakEnergyImport);
                Assert.AreEqual(123, result.OffPeakEnergyImport);
                Assert.AreEqual(321, result.ShoulderEnergyImport);
                Assert.AreEqual(456, result.HighShoulderEnergyImport);
                Assert.AreEqual(15197, result.Insolation);
            });
        }

        [Test]
        public async Task TeamOutputreader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<ITeamOutput>();
            ITeamOutput result = await reader.ReadObjectAsync(new StringReader(TEAMOUTPUT_RESPONSE_DAY));

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.Date);
                Assert.AreEqual(980, result.Outputs);
                Assert.AreEqual(4.736, result.Efficiency);
                Assert.AreEqual(19264849, result.TotalGeneration);
                Assert.AreEqual(19658, result.AverageGeneration);
                Assert.AreEqual(5116604, result.TotalExported);
                Assert.AreEqual(3815116, result.TotalConsumption);
                Assert.AreEqual(3893, result.AverageConsumption);
                Assert.AreEqual(2224276, result.TotalImported);
            });
        }

        [Test]
        public async Task AggregatedOutputreader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<IAggregatedOutput>();
            IAggregatedOutput result = await reader.ReadObjectAsync(new StringReader(AGGREGATEDOUTPUT_RESPONSE_SINGLE));

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 6, 1), result.Date);
                Assert.AreEqual(30, result.Outputs);
                Assert.AreEqual(420107, result.EnergyGenerated);
                Assert.AreEqual(3.395m, result.Efficiency);
                Assert.AreEqual(301293, result.EnergyExported);
                Assert.AreEqual(274773, result.EnergyUsed);
                Assert.AreEqual(155959, result.PeakEnergyImport);
                Assert.AreEqual(123, result.OffPeakEnergyImport);
                Assert.AreEqual(321, result.ShoulderEnergyImport);
                Assert.AreEqual(456, result.HighShoulderEnergyImport);
            });
        }
    }
}
