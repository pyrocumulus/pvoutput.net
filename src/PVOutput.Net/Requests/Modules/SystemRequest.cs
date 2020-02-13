using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class SystemRequest : GetRequest<ISystem>
    {
        public int? SystemId { get; set; }

        public bool SecondaryArray { get; set; } = true;
        public bool TariffDetails { get; set; } = true;
        public bool Teams { get; set; } = true;
        public bool MonthlyEstimates { get; set; } = true;
        public bool DonationCount { get; set; } = true;
        public bool ExtendedDataConfig { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getsystem.jsp{?array2,tariffs,teams,est,donations,sid1,ext}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["array2"] = SecondaryArray ? 1 : 0,
            ["tariffs"] = TariffDetails ? 1 : 0,
            ["teams"] = Teams ? 1 : 0,
            ["est"] = MonthlyEstimates ? 1 : 0,
            ["donations"] = DonationCount ? 1 : 0,
            ["sid1"] = SystemId,
            ["ext"] = ExtendedDataConfig ? 1 : 0
        };
    }
}
