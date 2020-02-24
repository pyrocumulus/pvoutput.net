using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class GetDayStatisticsRequest : GetRequest<IDayStatistics>
    {
        public DateTime Date { get; set; }
        public int? SystemId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getstatus.jsp{?d,from,to,sid1,stats}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["sid1"] = SystemId,
            ["d"] = FormatHelper.GetDateAsString(Date),
            ["from"] = FormatHelper.GetTimeAsString(From),
            ["to"] = FormatHelper.GetTimeAsString(To),
            ["stats"] = 1
        };
    }
}
