using System;
using System.Collections.Generic;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class StatisticPeriodRequest : StatisticRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = base.GetUriPathParameters();
            parameters["df"] = FormatHelper.GetDateAsString(FromDate);
            parameters["dt"] = FormatHelper.GetDateAsString(ToDate);
            return parameters;
        }
    }
}
