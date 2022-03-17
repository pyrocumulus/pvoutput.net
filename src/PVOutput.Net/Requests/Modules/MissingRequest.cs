using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class MissingRequest : GetRequest<IMissing>
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
