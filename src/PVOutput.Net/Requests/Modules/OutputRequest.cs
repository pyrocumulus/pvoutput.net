using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class OutputRequest : GetRequest<IOutput>
    {
        public int? SystemId { get; set; }
        public int? TeamId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Insolation { get; set; }
        public AggregationPeriod? Aggregation { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getoutput.jsp{?sid1,tid,df,dt,insolation,a}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("sid1", SystemId);
            parameters.AddIfNotNull("tid", TeamId);
            parameters.AddIfNotNull("a", GetAggregationParameter(Aggregation));
            parameters.AddIfNotNull("df", FormatHelper.GetDateAsString(FromDate));
            parameters.AddIfNotNull("dt", FormatHelper.GetDateAsString(ToDate));
            parameters.AddIfNotNull("insolation", Insolation ? 1 : 0);
            return parameters;
        }

        private static string? GetAggregationParameter(AggregationPeriod? aggregationPeriod)
        {
            if (aggregationPeriod == null)
            {
                return null!;
            }

            return aggregationPeriod == AggregationPeriod.Month ? "m" : "y";
        }
    }
}
