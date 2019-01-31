using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Output
{
    [TestFixture]
    public class OutputServiceTests
    {
        //TODO: vastly improve tests; split by object parsing (deserializing) and just testing the correct response is returned

        [Test]
        public async Task OutputService_WithBareData_GetDaily()
        {
            DateTime mockDate = new DateTime(2016, 10, 1);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.OUTPUT_RESPONSE_BARE);
            var response = await client.Output.GetOutputForDateAsync(mockDate, false);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var output = response.Value;
            Assert.AreEqual(mockDate, output.Date);
        }


        [Test]
        public async Task OutputService_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.OUTPUT_RESPONSE_DAY);
            var response = await client.Output.GetOutputForDateAsync(mockDate, false);

            if (response.Exception != null)
                throw response.Exception;

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

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.OUTPUT_RESPONSE_WEEK);
            var response = await client.Output.GetOutputsForPeriodAsync(fromDate, toDate, false);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Value.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }


        [Test]
        public async Task OutputService_WithInsolation_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.OUTPUT_WITH_INSOLATION_RESPONSE_DAY);
            var response = await client.Output.GetOutputForDateAsync(mockDate, true);

            if (response.Exception != null)
                throw response.Exception;

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

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.OUTPUT_WITH_INSOLATION_RESPONSE_WEEK);
            var response = await client.Output.GetOutputsForPeriodAsync(fromDate, toDate, true);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Value.OrderBy((o) => o.Date);

            Assert.AreEqual(15197, outputs.First().Insolation);
            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }

        [Test]
        public async Task OutputService_Team_GetDaily()
        {
            DateTime mockDate = new DateTime(2018, 9, 1);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.TEAMOUTPUT_RESPONSE_DAY);
            var response = await client.Output.GetTeamOutputForDateAsync(mockDate, TestConstants.PVOUTPUT_TEAM_ID);

            if (response.Exception != null)
                throw response.Exception;

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

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.TEAMOUTPUT_RESPONSE_WEEK);
            var response = await client.Output.GetTeamOutputsForPeriodAsync(fromDate, toDate, TestConstants.PVOUTPUT_TEAM_ID);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Value.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate, outputs.First().Date);
            Assert.AreEqual(toDate, outputs.Last().Date);
            Assert.AreEqual(7, outputs.Count());
        }

        [Test]
        public async Task OutputService_Aggregated_GetByMonth()
        {
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 6, 30);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.AGGREGATEDOUTPUT_RESPONSE_MONTH);
            var response = await client.Output.GetAggregatedOutputsAsync(fromDate, toDate, Net.Requests.Outputs.AggregationPeriod.Month);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Value.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate.Month, outputs.First().Date.Month);
            Assert.AreEqual(toDate.Month, outputs.Last().Date.Month);
            Assert.AreEqual(6, outputs.Count());
        }

        [Test]
        public async Task OutputService_Aggregated_GetByYear()
        {
            DateTime fromDate = new DateTime(2016, 1, 1);
            DateTime toDate = new DateTime(2018, 12, 31);

            var client = TestUtility.GetMockClient(OutputServiceTestsData.GETOUTPUT_URL, OutputServiceTestsData.AGGREGATEDOUPUT_RESPONSE_YEAR);
            var response = await client.Output.GetAggregatedOutputsAsync(fromDate, toDate, Net.Requests.Outputs.AggregationPeriod.Year);

            if (response.Exception != null)
                throw response.Exception;

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.IsSuccess);

            var outputs = response.Value.OrderBy((o) => o.Date);

            Assert.AreEqual(fromDate.Year, outputs.First().Date.Year);
            Assert.AreEqual(toDate.Year, outputs.Last().Date.Year);
            Assert.AreEqual(3, outputs.Count());
        }

    }
}
