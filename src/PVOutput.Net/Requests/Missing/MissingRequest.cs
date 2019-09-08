using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Missing;
using PVOutput.Net.Requests.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PVOutput.Net.Requests.Missing
{
	internal class MissingRequest : GetRequest<IMissing>
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public override HttpMethod Method => HttpMethod.Get;

		public override string UriTemplate => "getmissing.jsp{?df,dt}";

		public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
		{
			["df"] = FormatHelper.GetDateAsString(FromDate),
			["dt"] = FormatHelper.GetDateAsString(ToDate)
		};
	}
}
