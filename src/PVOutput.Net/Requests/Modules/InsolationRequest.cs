using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class InsolationRequest : GetRequest<IInsolation>
    {
        public DateTime? Date { get; set; }
        public int? SystemId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getinsolation.jsp{?d,ll,sid1}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["d"] = Date != null ? FormatHelper.GetDateAsString(Date.Value) : null,
            ["ll"] = FormatHelper.GetLocationAsString(Latitude, Longitude),
            ["sid1"] = SystemId
        };
    }
}
