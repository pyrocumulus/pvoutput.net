using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class GetStatusRequest : GetRequest<IStatus>
    {
        public int? SystemId { get; set; }
        public DateTime Date { get; set; }
        public bool History { get; set; }
        public bool Ascending { get; set; }
        public int? Limit { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Extended { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getstatus.jsp{?d,t,h,asc,limit,from,to,ext,sid1}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("sid1", SystemId);
            parameters.AddIfNotNull("d", FormatHelper.GetDateAsString(Date));
            parameters.AddIfNotNull("t", FormatHelper.GetTimeAsString(Date));
            parameters.AddIfNotNull("h", History ? 1 : 0);
            parameters.AddIfNotNull("asc", Ascending ? 1 : 0);
            parameters.AddIfNotNull("from", FormatHelper.GetTimeAsString(From));
            parameters.AddIfNotNull("to", FormatHelper.GetTimeAsString(To));
            parameters.AddIfNotNull("ext", Extended ? 1 : 0);
            parameters.AddIfNotNull("limit", Limit);
            return parameters;
        }
    }
}
