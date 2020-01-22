using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class LeaveTeamRequest : PostRequest
    {
        public int TeamId { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "leaveteam.jsp{?tid}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["tid"] = TeamId
        };
    }
}
