using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PVOutput.Net.Requests.Modules
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
}
