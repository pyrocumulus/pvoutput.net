using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class GetDayStatisticsRequest : GetRequest<IDayStatistics>
    {
        public DateTime Date { get; set; }
        public int? SystemId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getstatus.jsp{?d,from,to,sid1,stats}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("sid1", SystemId);
            parameters.AddIfNotNull("d", FormatHelper.GetDateAsString(Date));
            parameters.AddIfNotNull("from", FormatHelper.GetTimeAsString(From));
            parameters.AddIfNotNull("to", FormatHelper.GetTimeAsString(To));
            parameters.AddIfNotNull("stats", 1);
            return parameters;
        }
    }
}
