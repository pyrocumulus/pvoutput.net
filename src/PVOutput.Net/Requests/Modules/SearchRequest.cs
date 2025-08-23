using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class SearchRequest : GetRequest<ISystemSearchResult>
    {
        public required string SearchQuery { get; set; }
        public PVCoordinate? Coordinate { get; set; }
        public string? CountryCode { get; internal set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "search.jsp{?q,ll,country,country_code}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("q", SearchQuery);
            parameters.AddIfNotNull("ll", Coordinate?.ToString());
            parameters.AddIfNotNull("country_code", CountryCode);
            parameters.AddIfNotNull("country", 1);
            return parameters;
        }
    }
}
