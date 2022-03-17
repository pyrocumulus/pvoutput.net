using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class FavouriteRequest : GetRequest<IFavourite>
    {
        public int? SystemId { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getfavourite.jsp{?sid1}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["sid1"] = SystemId
        };
    }
}
