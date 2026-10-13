using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class AddBatchNetStatusRequest : PostRequest
    {
        public required IEnumerable<IBatchNetStatusPost> StatusPosts { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addbatchstatus.jsp{?n,data}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("n", 1);
            parameters.AddIfNotNull("data", FormatStatusPosts());
            return parameters;
        }

        private string FormatStatusPosts()
        {
            var sb = new StringBuilder();

            foreach (IBatchNetStatusPost status in StatusPosts)
            {
                sb.Append(FormatStatusPost(status)).Append(';');
            }

            return sb.ToString();
        }

        internal static string FormatStatusPost(IBatchNetStatusPost status)
        {
            var sb = new StringBuilder();
            sb.Append(FormatHelper.GetDateAsString(status.Timestamp));
            sb.Append(',');
            sb.Append(FormatHelper.GetTimeAsString(status.Timestamp));
            sb.Append(",-1,"); // Skip single field as per documentation
            sb.Append(status.PowerExported);
            sb.Append(",-1,"); // Skip single field as per documentation
            sb.Append(status.PowerImported);
            return sb.ToString();
        }
    }
}
