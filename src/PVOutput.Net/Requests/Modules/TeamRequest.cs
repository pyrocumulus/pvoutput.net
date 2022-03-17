using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class TeamRequest : GetRequest<ITeam>
    {
        public int TeamId { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getteam.jsp{?tid}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["tid"] = TeamId
        };
    }
}
