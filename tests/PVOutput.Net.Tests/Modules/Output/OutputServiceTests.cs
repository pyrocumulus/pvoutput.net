using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Builders;
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

        [Test]
        public void OutputService_GetOutputForDate_WithFutureDate_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetOutputForDateAsync(DateTime.Today.AddDays(1));
            });
        }

        [Test]
        public void OutputService_GetOutputsForPeriod_WithFutureRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetOutputsForPeriodAsync(DateTime.Today, DateTime.Today.AddDays(1));
            });
        }

        [Test]
        public void OutputService_GetOutputsForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetOutputsForPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29));
            });
        }

        [Test]
        public void OutputService_GetTeamOutputForDate_WithFutureDate_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetTeamOutputForDateAsync(DateTime.Today.AddDays(1), 1234);
            });
        }

        [Test]
        public void OutputService_GetTeamOutputsForPeriod_WithFutureRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetTeamOutputsForPeriodAsync(DateTime.Today, DateTime.Today.AddDays(1), 1234);
            });
        }

        [Test]
        public void OutputService_GetTeamOutputsForPeriod_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetTeamOutputsForPeriodAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29), 1234);
            });
        }

        [Test]
        public void OutputService_GetAggregatedOutputs_WithFutureRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetAggregatedOutputsAsync(DateTime.Today, DateTime.Today.AddDays(1), AggregationPeriod.Month);
            });
        }

        [Test]
        public void OutputService_GetAggregatedOutputs_WithReversedRange_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                _ = await client.Output.GetAggregatedOutputsAsync(new DateTime(2016, 8, 30), new DateTime(2016, 8, 29), AggregationPeriod.Month);
            });
        }

        /* 
            Adding outputs
        */

        [Test]
        public void OutputService_AddOutput_WithNullOutput_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                _ = await client.Output.AddOutputAsync(null);
            });
        }

        [Test]
        public void OutputService_AddBatchOutput_WithNullOutputs_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                _ = await client.Output.AddOutputsAsync(null);
            });
        }

        [Test]
        public void OutputService_AddBatchOutput_WithEmptyOutputs_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _ = await client.Output.AddOutputsAsync(new List<IOutputPost>());
            });
        }

        public static IEnumerable AddOutputTestCases
        {
            get
            {
                yield return new TestCaseData(new OutputPostBuilder()
                    .SetDate(new DateTime(2020, 1, 1)).SetEnergyGenerated(11000).SetEnergyExported(9000).Build(), "d=20200101&g=11000&e=9000");

                yield return new TestCaseData(new OutputPostBuilder()
                    .SetDate(new DateTime(2020, 1, 1)).SetPeakPower(6500).SetPeakTime(new TimeSpan(10, 10, 0)).Build(), "d=20200101&pp=6500&pt=10:10");

                yield return new TestCaseData(new OutputPostBuilder()
                    .SetDate(new DateTime(2020, 1, 1)).SetTemperatures(11.2m, 17.8m).Build(), "d=20200101&tm=11.2&tx=17.8");

                yield return new TestCaseData(new OutputPostBuilder()
                    .SetDate(new DateTime(2020, 1, 1)).SetCondition(WeatherCondition.PartlyCloudy).SetComments("Test").Build(), "d=20200101&cd=Partly%20Cloudy&cm=Test");

                yield return new TestCaseData(new OutputPostBuilder()
                    .SetDate(new DateTime(2020, 1, 1)).SetPeakEnergyImport(1200).SetOffPeakEnergyImport(1300).SetShoulderEnergyImport(1400).SetHighShoulderEnergyImport(1500)
                    .Build(), 
                    "d=20200101&ip=1200&io=1300&is=1400&ih=1500");
            }
        }

        [TestCaseSource(typeof(OutputServiceTests), nameof(AddOutputTestCases))]
        public async Task OutputService_AddOutput_CallsCorrectUri(IOutputPost outputToPost, string expectedQueryString)
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(ADDOUTPUT_URL)
                        .WithQueryString(expectedQueryString)
                        .RespondPlainText("");

            await client.Output.AddOutputAsync(outputToPost);
            testProvider.VerifyNoOutstandingExpectation();
        }


        [Test]
        public async Task OutputService_AddBatchOutput_SendsCorrectContent()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(ADDOUTPUT_URL)
                        .WithQueryString("data=20200101,11000,9000,,,,,,,,,,,,2233,,,;20200101,,,,,Partly Cloudy,,,Test,,,,,12000,,,,2121;")
                        .RespondPlainText("");

            var builder = new OutputPostBuilder();
            var outputs = new List<IOutputPost>();

            outputs.Add(builder.SetDate(new DateTime(2020, 1, 1)).SetEnergyGenerated(11000).SetEnergyExported(9000).SetPeakEnergyExport(2233).BuildAndReset());
            outputs.Add(builder.SetDate(new DateTime(2020, 1, 1)).SetConsumption(12000).SetCondition(WeatherCondition.PartlyCloudy).SetComments("Test").SetHighShoulderEnergyExport(2121).BuildAndReset());

            await client.Output.AddOutputsAsync(outputs);
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
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OutputDate, Is.EqualTo(new DateTime(2016, 10, 1)));
                Assert.That(result.EnergyGenerated, Is.EqualTo(8190));
                Assert.That(result.Efficiency, Is.EqualTo(1.985));
                Assert.That(result.EnergyExported, Is.EqualTo(0));
                Assert.That(result.EnergyUsed, Is.EqualTo(0));
                Assert.That(result.PeakPower, Is.Null);
                Assert.That(result.PeakTime, Is.Null);
                Assert.That(result.Condition, Is.EqualTo(WeatherCondition.Cloudy));
                Assert.That(result.MinimumTemperature, Is.Null);
                Assert.That(result.MaximumTemperature, Is.Null);
                Assert.That(result.PeakEnergyImport, Is.Null);
                Assert.That(result.OffPeakEnergyImport, Is.Null);
                Assert.That(result.ShoulderEnergyImport, Is.Null);
                Assert.That(result.HighShoulderEnergyImport, Is.Null);
            });
        }

        [Test]
        public async Task OutputReader_ForDayResponse_CreatesCorrectObject()
        {
            IOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IOutput>(OUTPUT_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(result.EnergyGenerated, Is.EqualTo(16784));
                Assert.That(result.Efficiency, Is.EqualTo(4.069));
                Assert.That(result.EnergyExported, Is.EqualTo(12719));
                Assert.That(result.EnergyUsed, Is.EqualTo(8500));
                Assert.That(result.PeakPower, Is.EqualTo(3422));
                Assert.That(result.PeakTime, Is.EqualTo(new TimeSpan(12, 0, 0)));
                Assert.That(result.Condition, Is.EqualTo(WeatherCondition.Fine));
                Assert.That(result.MinimumTemperature, Is.EqualTo(7));
                Assert.That(result.MaximumTemperature, Is.EqualTo(23));
                Assert.That(result.PeakEnergyImport, Is.EqualTo(4435));
                Assert.That(result.OffPeakEnergyImport, Is.EqualTo(123));
                Assert.That(result.ShoulderEnergyImport, Is.EqualTo(321));
                Assert.That(result.HighShoulderEnergyImport, Is.EqualTo(456));
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
                Assert.That(result, Has.Exactly(7).Items);
                Assert.That(firstOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 7)));
                Assert.That(firstOutput.Condition, Is.EqualTo(WeatherCondition.PartlyCloudy));
                Assert.That(lastOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(lastOutput.Condition, Is.EqualTo(WeatherCondition.Fine));
            });
        }

        [Test]
        public async Task OutputReader_ForResponseWithInsolation_CreatesCorrectObject()
        {
            IOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IOutput>(OUTPUT_WITH_INSOLATION_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(result.EnergyGenerated, Is.EqualTo(16784));
                Assert.That(result.Efficiency, Is.EqualTo(4.069));
                Assert.That(result.EnergyExported, Is.EqualTo(12719));
                Assert.That(result.EnergyUsed, Is.EqualTo(8500));
                Assert.That(result.PeakPower, Is.EqualTo(3422));
                Assert.That(result.PeakTime, Is.EqualTo(new TimeSpan(12, 0, 0)));
                Assert.That(result.Condition, Is.EqualTo(WeatherCondition.Fine));
                Assert.That(result.MinimumTemperature, Is.EqualTo(7));
                Assert.That(result.MaximumTemperature, Is.EqualTo(23));
                Assert.That(result.PeakEnergyImport, Is.EqualTo(4435));
                Assert.That(result.OffPeakEnergyImport, Is.EqualTo(123));
                Assert.That(result.ShoulderEnergyImport, Is.EqualTo(321));
                Assert.That(result.HighShoulderEnergyImport, Is.EqualTo(456));
                Assert.That(result.Insolation, Is.EqualTo(15197));
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
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Exactly(7).Items);
                Assert.That(firstOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 7)));
                Assert.That(lastOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(firstOutput.Insolation, Is.EqualTo(14189));
                Assert.That(lastOutput.Insolation, Is.EqualTo(15197));
            });
        }

        [Test]
        public async Task TeamOutputReader_ForResponse_CreatesCorrectObject()
        {
            ITeamOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<ITeamOutput>(TEAMOUTPUT_RESPONSE_DAY);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(result.Outputs, Is.EqualTo(980));
                Assert.That(result.Efficiency, Is.EqualTo(4.736));
                Assert.That(result.TotalGeneration, Is.EqualTo(19264849));
                Assert.That(result.AverageGeneration, Is.EqualTo(19658));
                Assert.That(result.TotalExported, Is.EqualTo(5116604));
                Assert.That(result.TotalConsumption, Is.EqualTo(3815116));
                Assert.That(result.AverageConsumption, Is.EqualTo(3893));
                Assert.That(result.TotalImported, Is.EqualTo(2224276));
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
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Exactly(7).Items);
                Assert.That(firstOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 7)));
                Assert.That(firstOutput.TotalGeneration, Is.EqualTo(15628240));
                Assert.That(lastOutput.OutputDate, Is.EqualTo(new DateTime(2018, 9, 1)));
                Assert.That(lastOutput.TotalGeneration, Is.EqualTo(19264849));
            });
        }

        [Test]
        public async Task AggregatedOutputReader_ForResponse_CreatesCorrectObject()
        {
            IAggregatedOutput result = await TestUtility.ExecuteObjectReaderByTypeAsync<IAggregatedOutput>(AGGREGATEDOUTPUT_RESPONSE_SINGLE);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.AggregatedDate, Is.EqualTo(new DateTime(2018, 6, 1)));
                Assert.That(result.Outputs, Is.EqualTo(30));
                Assert.That(result.EnergyGenerated, Is.EqualTo(420107));
                Assert.That(result.Efficiency, Is.EqualTo(3.395m));
                Assert.That(result.EnergyExported, Is.EqualTo(301293));
                Assert.That(result.EnergyUsed, Is.EqualTo(274773));
                Assert.That(result.PeakEnergyImport, Is.EqualTo(155959));
                Assert.That(result.OffPeakEnergyImport, Is.EqualTo(123));
                Assert.That(result.ShoulderEnergyImport, Is.EqualTo(321));
                Assert.That(result.HighShoulderEnergyImport, Is.EqualTo(456));
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
                Assert.That(result, Is.Not.Null);
                Assert.That(firstAggregate.AggregatedDate.Month, Is.EqualTo(6));
                Assert.That(lastAggregate.AggregatedDate.Month, Is.EqualTo(1));
                Assert.That(result, Has.Exactly(6).Items);
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
                Assert.That(result, Is.Not.Null);
                Assert.That(firstAggregate.AggregatedDate.Year, Is.EqualTo(2018));
                Assert.That(lastAggregate.AggregatedDate.Year, Is.EqualTo(2016));
                Assert.That(result, Has.Exactly(3).Items);
            });
        }
    }
}
