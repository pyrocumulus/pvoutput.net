using NUnit.Framework;
using PVOutput.Net.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Modules.Missing
{
	[TestFixture]
	public class MissingServiceTests
	{
		[Test]
		public async Task MissingService_GetMissingDaysInPeriod_WithResult()
		{
			var client = TestUtility.GetMockClient(MissingTestsData.GETMISSING_URL, MissingTestsData.MISSINGDATES_RESPONSE_SIMPLE);
			var response = await client.Missing.GetMissingDaysInPeriod(new DateTime(2016, 8, 1), new DateTime(2016, 8, 30));

			if (response.Exception != null)
				throw response.Exception;

			Assert.IsTrue(response.HasValue);
			Assert.IsNotNull(response.IsSuccess);

			var missing = response.Value;
			Assert.AreEqual(21, missing.Dates.Count());
			Assert.AreEqual(new DateTime(2016, 8, 1), missing.Dates.First());
			Assert.AreEqual(new DateTime(2016, 8, 21), missing.Dates.Last());
		}

		[Test]
		public async Task MissingService_GetMissingDaysInPeriod_NoResult()
		{
			var client = TestUtility.GetMockClient(MissingTestsData.GETMISSING_URL, MissingTestsData.MISSINGDATES_RESPONSE_NONE);
			var response = await client.Missing.GetMissingDaysInPeriod(new DateTime(2016, 8, 1), new DateTime(2016, 8, 30));

			if (response.Exception != null)
				throw response.Exception;

			Assert.IsTrue(response.HasValue);
			Assert.IsNotNull(response.IsSuccess);

			var missing = response.Value;
			Assert.IsNotNull(missing.Dates);
			Assert.AreEqual(0, missing.Dates.Count());
		}
	}
}
