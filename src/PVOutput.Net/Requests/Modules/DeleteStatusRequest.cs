using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class DeleteStatusRequest : PostRequest
    {
        public DateTime Timestamp { get; set; }

        public bool CompleteDate { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "deletestatus.jsp{?d,t}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("d", FormatHelper.GetDateAsString(Timestamp));
            parameters.AddIfNotNull("t", CompleteDate ? null : FormatHelper.GetTimeAsString(Timestamp));
            return parameters;
        }
    }
}
