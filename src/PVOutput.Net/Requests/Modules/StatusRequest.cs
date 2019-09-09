using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PVOutput.Net.Requests.Modules
{
	internal class StatusRequest : GetRequest<IStatus>
	{

		public int? SystemId { get; set; }
		public DateTime Date { get; set; }
		public bool History { get; set; }
		public bool Ascending { get; set; }
		public int? Limit { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public bool Extended { get; set; }

		public override HttpMethod Method => HttpMethod.Get;

		public override string UriTemplate => "getstatus.jsp{?d,t,h,asc,limit,from,to,ext,sid1}";

		public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
		{
			["sid1"] = SystemId,
			["d"] = FormatHelper.GetDateAsString(Date),
			["t"] = FormatHelper.GetTimeAsString(Date),
			["history"] = History ? 1 : 0,
			["asc"] = Ascending ? 1 : 0,
			["from"] = FormatHelper.GetTimeAsString(From),
			["to"] = FormatHelper.GetTimeAsString(To),
			["ext"] = Extended ? 1 : 0,
			["limit"] = Limit
		};
	}
}
