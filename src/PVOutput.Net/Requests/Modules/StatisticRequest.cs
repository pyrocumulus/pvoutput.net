using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Requests.Modules
{
    internal class StatisticRequest : GetRequest<IStatistic>
    {
        public int? SystemId { get; set; }
        public bool IncludeConsumptionImport { get; set; }
        public bool IncludeCreditDebit { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getstatistic.jsp{?df,dt,c,cdr,sid1}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("sid1", SystemId);
            parameters.AddIfNotNull("c", IncludeConsumptionImport ? 1 : 0);
            parameters.AddIfNotNull("cdr", IncludeCreditDebit ? 1 : 0);
            return parameters;
        }
    }
}
