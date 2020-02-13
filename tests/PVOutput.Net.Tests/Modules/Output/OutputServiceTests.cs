using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Output
{
    [TestFixture]
    public partial class OutputServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task OutputService_ForDate_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20161001&dt=20161001&insolation=0")
                        .RespondPlainText(OUTPUT_RESPONSE_BARE);

            var response = await client.Output.GetOutputForDateAsync(new DateTime(2016, 10, 1), false);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_ForPeriod_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180901&dt=20180907&insolation=0")
                        .RespondPlainText(OUTPUT_RESPONSE_WEEK);

            var response = await client.Output.GetOutputsForPeriodAsync(new DateTime(2018, 9, 1), new DateTime(2018, 9, 7), false);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }


        [Test]
        public async Task OutputService_WithDailyInsolation_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180901&dt=20180901&insolation=1")
                        .RespondPlainText(OUTPUT_WITH_INSOLATION_RESPONSE_DAY);

            var response = await client.Output.GetOutputForDateAsync(new DateTime(2018, 9, 1), true);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_WithWeeklyInsolation_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180901&dt=20180907&insolation=1")
                        .RespondPlainText(OUTPUT_WITH_INSOLATION_RESPONSE_WEEK);

            var response = await client.Output.GetOutputsForPeriodAsync(new DateTime(2018, 9, 1), new DateTime(2018, 9, 7), true);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_GetDailyTeam_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180901&dt=20180901&tid=" + TestConstants.PVOUTPUT_TEAM_ID)
                        .RespondPlainText(TEAMOUTPUT_RESPONSE_DAY);

            var response = await client.Output.GetTeamOutputForDateAsync(new DateTime(2018, 9, 1), TestConstants.PVOUTPUT_TEAM_ID);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_GetWeeklyTeam_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180901&dt=20180907&tid=" + TestConstants.PVOUTPUT_TEAM_ID)
                        .RespondPlainText(TEAMOUTPUT_RESPONSE_WEEK);

            var response = await client.Output.GetTeamOutputsForPeriodAsync(new DateTime(2018, 9, 1), new DateTime(2018, 9, 7), TestConstants.PVOUTPUT_TEAM_ID);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_GetAggregatedByMonth_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20180101&dt=20180630&a=m")
                        .RespondPlainText(AGGREGATEDOUTPUT_RESPONSE_MONTH);

            var response = await client.Output.GetAggregatedOutputsAsync(new DateTime(2018, 1, 1), new DateTime(2018, 6, 30), AggregationPeriod.Month);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task OutputService_GetAggregatedByYear_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(GETOUTPUT_URL)
                        .WithQueryString("df=20160101&dt=20181231&a=y")
                        .RespondPlainText(AGGREGATEDOUTPUT_RESPONSE_YEAR);

            var response = await client.Output.GetAggregatedOutputsAsync(new DateTime(2016, 1, 1), new DateTime(2018, 12, 31), AggregationPeriod.Year);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /* 
            Adding outputs
        */

        public static IEnumerable AddOutputTestCases
        {
            get
            {
                yield return new TestCaseData(new OutputPostBuilder<IOutputPost>()
                    .SetDate(new DateTime(2020, 1, 1)).SetGenerated(11000).SetExported(9000).Build(), "d=20200101&g=11000&e=9000");

                yield return new TestCaseData(new OutputPostBuilder<IOutputPost>()
                    .SetDate(new DateTime(2020, 1, 1)).SetPeakPower(6500).SetPeakTime(new DateTime(2020, 1, 1, 10, 10, 0)).Build(), "d=20200101&pp=6500&pt=10:10");

                yield return new TestCaseData(new OutputPostBuilder<IOutputPost>()
                    .SetDate(new DateTime(2020, 1, 1)).SetTemperatures(11.2m, 17.8m).Build(), "d=20200101&tm=11.2&tx=17.8");

                yield return new TestCaseData(new OutputPostBuilder<IOutputPost>()
                    .SetDate(new DateTime(2020, 1, 1)).SetCondition("Fine").SetComments("Test").Build(), "d=20200101&cd=Fine&cm=Test");

                yield return new TestCaseData(new OutputPostBuilder<IOutputPost>()
                    .SetDate(new DateTime(2020, 1, 1)).SetPeakEnergyImport(1200).SetOffPeakEnergyImport(1300).SetShoulderEnergyImport(1400).SetHighShoulderEnergyImport(1500)
                    .Build(), 
                    "d=20200101&ip=1200&io=1300&is=1400&ih=1500");
            }
        }

        [Test]
        [TestCaseSource(typeof(OutputServiceTests), "AddOutputTestCases")]
        public async Task OutputService_AddOutput_CallsCorrectUri(IOutputPost outputToPost, string expectedQueryString)
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(ADDOUTPUT_URL)
                        .WithQueryString(expectedQueryString)
                        .RespondPlainText("");

            await client.Output.AddOutputAsync(outputToPost);
            testProvider.VerifyNoOutstandingExpectation();
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task OutputReader_ForBareResponse_CreatesCorrectObject()
        {
            IOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IOutput>(OUTPUT_RESPONSE_BARE);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2016, 10, 1), result.OutputDate);
                Assert.AreEqual(8190, result.EnergyGenerated);
                Assert.AreEqual(1.985, result.Efficiency);
                Assert.AreEqual(0, result.EnergyExported);
                Assert.AreEqual(0, result.EnergyUsed);
                Assert.IsNull(result.PeakPower);
                Assert.IsNull(result.PeakTime);
                Assert.AreEqual("Cloudy", result.Condition);
                Assert.IsNull(result.MinimumTemperature);
                Assert.IsNull(result.MaximumTemperature);
                Assert.IsNull(result.PeakEnergyImport);
                Assert.IsNull(result.OffPeakEnergyImport);
                Assert.IsNull(result.ShoulderEnergyImport);
                Assert.IsNull(result.HighShoulderEnergyImport);
            });
        }

        [Test]
        public async Task OutputReader_ForDayResponse_CreatesCorrectObject()
        {
            IOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IOutput>(OUTPUT_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.OutputDate);
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
        public async Task OutputReader_ForPeriodResponse_CreatesCorrectObject()
        {
            IEnumerable<IOutput> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IOutput>(OUTPUT_RESPONSE_WEEK);

            var firstOutput = result.First();
            var lastOutput = result.Last();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(7, result.Count());
                Assert.AreEqual(new DateTime(2018, 9, 7), firstOutput.OutputDate);
                Assert.AreEqual(new DateTime(2018, 9, 1), lastOutput.OutputDate);
            });
        }

        [Test]
        public async Task OutputReader_ForResponseWithInsolation_CreatesCorrectObject()
        {
            IOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IOutput>(OUTPUT_WITH_INSOLATION_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.OutputDate);
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
        public async Task OutputReader_ForPeriodResponseWithInsolation_CreatesCorrectObjects()
        {
            IEnumerable<IOutput> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IOutput>(OUTPUT_WITH_INSOLATION_RESPONSE_WEEK);

            var firstOutput = result.First();
            var lastOutput = result.Last();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(7, result.Count());
                Assert.AreEqual(new DateTime(2018, 9, 7), firstOutput.OutputDate);
                Assert.AreEqual(new DateTime(2018, 9, 1), lastOutput.OutputDate);
                Assert.AreEqual(14189, firstOutput.Insolation);
                Assert.AreEqual(15197, lastOutput.Insolation);
            });
        }

        [Test]
        public async Task TeamOutputReader_ForResponse_CreatesCorrectObject()
        {
            ITeamOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<ITeamOutput>(TEAMOUTPUT_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 9, 1), result.OutputDate);
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
        public async Task TeamOutputReader_ForPeriodResponse_CreatesCorrectObjects()
        {
            IEnumerable<ITeamOutput> result = await TestUtility.ExecuteArrayReaderByTypeAsync<ITeamOutput>(TEAMOUTPUT_RESPONSE_WEEK);

            var firstOutput = result.First();
            var lastOutput = result.Last();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(7, result.Count());
                Assert.AreEqual(new DateTime(2018, 9, 7), firstOutput.OutputDate);
                Assert.AreEqual(15628240, firstOutput.TotalGeneration);
                Assert.AreEqual(new DateTime(2018, 9, 1), lastOutput.OutputDate);
                Assert.AreEqual(19264849, lastOutput.TotalGeneration);
            });
        }

        [Test]
        public async Task AggregatedOutputReader_ForResponse_CreatesCorrectObject()
        {
            IAggregatedOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IAggregatedOutput>(AGGREGATEDOUTPUT_RESPONSE_SINGLE);

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(new DateTime(2018, 6, 1), result.AggregatedDate);
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

        [Test]
        public async Task AggregatedOutputReader_ForPeriodMonthAggregationResponse_CreatesCorrectObjects()
        {
            IEnumerable<IAggregatedOutput> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IAggregatedOutput>(AGGREGATEDOUTPUT_RESPONSE_MONTH);

            var firstAggregate = result.First();
            var lastAggregate = result.Last();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(6, firstAggregate.AggregatedDate.Month);
                Assert.AreEqual(1, lastAggregate.AggregatedDate.Month);
                Assert.AreEqual(6, result.Count());
            });
        }

        [Test]
        public async Task AggregatedOutputReader_ForPeriodYearAggregationResponse_CreatesCorrectObjects()
        {
            IEnumerable<IAggregatedOutput> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IAggregatedOutput>(AGGREGATEDOUTPUT_RESPONSE_YEAR);

            var firstAggregate = result.First();
            var lastAggregate = result.Last();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(2018, firstAggregate.AggregatedDate.Year);
                Assert.AreEqual(2016, lastAggregate.AggregatedDate.Year);
                Assert.AreEqual(3, result.Count());
            });
        }
    }
}
