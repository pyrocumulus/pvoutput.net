using System;
using System.Collections.Generic;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Requests.Modules
{
    internal class StatisticPeriodRequest : StatisticRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["sid1"] = SystemId,
            ["df"] = FormatHelper.GetDateAsString(FromDate),
            ["dt"] = FormatHelper.GetDateAsString(ToDate),
            ["c"] = IncludeConsumptionImport ? 1 : 0,
            ["cdr"] = IncludeCreditDebit ? 1 : 0
        };
    }
}
