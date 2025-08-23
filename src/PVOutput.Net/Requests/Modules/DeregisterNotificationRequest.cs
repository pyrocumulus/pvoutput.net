using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class DeregisterNotificationRequest : PostRequest
    {
        public required string ApplicationId { get; set; }
        public int? AlertType { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "deregisternotification.jsp{?appid,type}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("appid", ApplicationId);
            parameters.AddIfNotNull("type", AlertType ?? 0);
            return parameters;
        }
    }
}
