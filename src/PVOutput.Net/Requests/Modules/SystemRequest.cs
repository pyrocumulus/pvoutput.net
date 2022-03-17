using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class SystemRequest : GetRequest<ISystem>
    {
        public int? SystemId { get; set; }
        public bool MonthlyEstimates { get; set; } = true;

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getsystem.jsp{?array2,tariffs,teams,est,donations,sid1,ext}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["est"] = MonthlyEstimates ? 1 : 0,
            ["sid1"] = SystemId,

            // No need for options; we always request the following aspects
            // There is no negative side effect to this and it makes deserializing significantly easier

            // Request secondary array information
            ["array2"] = 1,

            // Request tariff details
            ["tariffs"] = 1,

            // Request team list
            ["teams"] = 1,

            // Request donation count
            ["donations"] = 1,

            // Request extended data configurations
            ["ext"] = 1
        };
    }
}
