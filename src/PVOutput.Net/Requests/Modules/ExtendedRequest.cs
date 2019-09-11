using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class ExtendedRequest : GetRequest<IExtended>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Limit { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getextended.jsp{?df,dt,limit}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["df"] = FromDate,
            ["dt"] = ToDate,
            ["limit"] = Limit
        };
    }
}
