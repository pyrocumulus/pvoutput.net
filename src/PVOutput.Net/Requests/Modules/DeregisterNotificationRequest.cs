using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class DeregisterNotificationRequest : PostRequest
    {
        public string ApplicationId { get; set; }
        public int? AlertType { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "deregisternotification.jsp{?appid,type}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            return new Dictionary<string, object>
            {
                ["appid"] = ApplicationId,
                ["type"] = AlertType ?? 0
            };
        }
    }
}
