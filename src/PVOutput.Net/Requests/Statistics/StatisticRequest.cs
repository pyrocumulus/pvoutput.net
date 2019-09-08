using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Statistics;
using PVOutput.Net.Requests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PVOutput.Net.Requests.Statistics
{
	internal class StatisticRequest : GetRequest<IStatistic>
	{
		public int? SystemId { get; set; }
		public bool IncludeConsumptionImport { get; set; }
		public bool IncludeCreditDebit { get; set; }

		public override HttpMethod Method => HttpMethod.Get;

		public override string UriTemplate => "getstatistic.jsp{?df,dt,c,cdr,sid1}";

		public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
		{
			["sid1"] = SystemId,
			["c"] = IncludeConsumptionImport ? 1 : 0,
			["cdr"] = IncludeCreditDebit ? 1 : 0
		};
	}

	internal class StatisticPeriodRequest : StatisticRequest
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
		{
			["sid1"] = SystemId,
			["df"] = FormatHelper.GetDateAsString(From),
			["dt"] = FormatHelper.GetDateAsString(To),
			["c"] = IncludeConsumptionImport ? 1 : 0,
			["cdr"] = IncludeCreditDebit ? 1 : 0
		};
	}
}
