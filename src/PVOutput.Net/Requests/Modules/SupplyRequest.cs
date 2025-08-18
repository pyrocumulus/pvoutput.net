using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class SupplyRequest : GetRequest<ISupply>
    {
        public string? TimeZone { get; set; }
        public string? RegionKey { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getsupply.jsp{?tz,r}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("tz", !string.IsNullOrEmpty(TimeZone) ? TimeZone : null);
            parameters.AddIfNotNull("r", !string.IsNullOrEmpty(RegionKey) ? RegionKey : null);
            return parameters;
        }
    }
}
