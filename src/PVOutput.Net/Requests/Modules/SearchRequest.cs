using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class SearchRequest : GetRequest<ISystemSearchResult>
    {
        public string SearchQuery { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "search.jsp{?q,ll,country}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["q"] = SearchQuery,
            ["ll"] = GetFormatLocation(),
            ["country"] = 1
        };

        private string GetFormatLocation()
        {
            if (Latitude == null || Longitude == null)
            {
                return null;
            }

            return string.Format("{0:N5},{1:N5}", Latitude, Longitude);
        }
    }
}
