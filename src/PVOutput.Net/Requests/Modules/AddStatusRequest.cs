using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class AddStatusRequest : PostRequest
    {
        public IStatusPost StatusPost { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addstatus.jsp{?d,t,v1,v2,v3,v4,v5,v6,c1,n,v7,v8,v9,v10,v11,v12,m1}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["d"] = FormatHelper.GetDateAsString(StatusPost.Date),
            ["t"] = FormatHelper.GetTimeAsString(StatusPost.Date),
            ["v1"] = StatusPost.EnergyGeneration,
            ["v2"] = StatusPost.PowerGeneration,
            ["v3"] = StatusPost.EnergyConsumption,
            ["v4"] = StatusPost.PowerConsumption,
            ["v5"] = FormatHelper.GetValueAsString(StatusPost.Temperature),
            ["v6"] = FormatHelper.GetValueAsString(StatusPost.Voltage),
            ["c1"] = StatusPost.Cumulative != Enums.CumulativeStatusType.None ? (int?)StatusPost.Cumulative : null,
            ["n"] = StatusPost.Net ? 1 : 0,
            ["v7"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue1),
            ["v8"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue2),
            ["v9"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue3),
            ["v10"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue4),
            ["v11"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue5),
            ["v12"] = FormatHelper.GetValueAsString(StatusPost.ExtendedValue6),
            ["m1"] = StatusPost.TextMessage
        };
    }
}
