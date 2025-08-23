using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class RegisterNotificationRequest : PostRequest
    {
        public required string ApplicationId { get; set; }
        public required System.Uri CallbackUri { get; set; }
        public int? AlertType { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "registernotification.jsp{?appid,url,type}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            return new Dictionary<string, object>
            {
                ["appid"] = ApplicationId,
                ["url"] = CallbackUri?.AbsoluteUri ?? "",
                ["type"] = AlertType ?? 0
            };
        }
    }
}
