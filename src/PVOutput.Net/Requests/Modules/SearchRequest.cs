using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class SearchRequest : GetRequest<ISystemSearchResult>
    {
        public string SearchQuery { get; set; }
        public PVCoordinate? Coordinate { get; set; }
        public string CountryCode { get; internal set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "search.jsp{?q,ll,country,country_code}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["q"] = SearchQuery,
            ["ll"] = Coordinate?.ToString(),
            ["country_code"] = CountryCode,
            ["country"] = 1
        };
    }
}
