using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class InsolationRequest : GetRequest<IInsolation>
    {
        public DateTime? Date { get; set; }
        public int? SystemId { get; set; }
        public PVCoordinate? Coordinate { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getinsolation.jsp{?d,ll,sid1}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["d"] = Date != null ? FormatHelper.GetDateAsString(Date.Value) : null,
            ["ll"] = Coordinate?.ToString(),
            ["sid1"] = SystemId
        };
    }
}
