using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    public enum AggregationPeriod { Month, Year };

    // TODO: Split requests per output interfacing type
    internal class OutputRequest : GetRequest<IOutput>
    {
        public int? SystemId { get; set; }
        public int? TeamId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Insolation { get; set; }
        public AggregationPeriod? AggregationPeriod { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getoutput.jsp{?sid1,tid,df,dt,insolation,a}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["sid1"] = SystemId,
            ["tid"] = TeamId,
            ["a"] = GetAggregationParameter(AggregationPeriod),
            ["df"] = FormatHelper.GetDateAsString(FromDate),
            ["dt"] = FormatHelper.GetDateAsString(ToDate),
            ["insolation"] = Insolation ? 1 : 0
        };

        private string GetAggregationParameter(AggregationPeriod? aggregationPeriod)
        {
            if (aggregationPeriod == null)
            {
                return null;
            }

            return aggregationPeriod == Modules.AggregationPeriod.Month ? "m" : "y";
        }
    }
}
