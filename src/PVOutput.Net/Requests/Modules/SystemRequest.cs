using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class SystemRequest : GetRequest<ISystem>
    {
        public int? SystemId { get; set; }
        public bool MonthlyEstimates { get; set; } = true;

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getsystem.jsp{?array2,tariffs,teams,est,donations,sid1,ext}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddIfNotNull("est", MonthlyEstimates ? 1 : 0);
            parameters.AddIfNotNull("sid1", SystemId);

            // No need for options; we always request the following aspects
            // There is no negative side effect to this and it makes deserializing significantly easier

            // Request secondary array information
            parameters.AddIfNotNull("array2", 1);

            // Request tariff details
            parameters.AddIfNotNull("tariffs", 1);

            // Request team list
            parameters.AddIfNotNull("teams", 1);

            // Request donation count
            parameters.AddIfNotNull("donations", 1);

            // Request extended data configurations
            parameters.AddIfNotNull("ext", 1);
            return parameters;
        }
    }
}
