using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class AddStatusRequest : PostRequest
    {
        public required IStatusPost StatusPost { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addstatus.jsp{?d,t,v1,v2,v3,v4,v5,v6,c1,n,v7,v8,v9,v10,v11,v12,m1}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();

            parameters.AddIfNotNull("d", FormatHelper.GetDateAsString(StatusPost.Timestamp));
            parameters.AddIfNotNull("t", FormatHelper.GetTimeAsString(StatusPost.Timestamp));
            parameters.AddIfNotNull("v1", StatusPost.EnergyGeneration);
            parameters.AddIfNotNull("v2", StatusPost.PowerGeneration);
            parameters.AddIfNotNull("v3", StatusPost.EnergyConsumption);
            parameters.AddIfNotNull("v4", StatusPost.PowerConsumption);
            parameters.AddIfNotNull("v5", FormatHelper.GetValueAsString(StatusPost.Temperature));
            parameters.AddIfNotNull("v6", FormatHelper.GetValueAsString(StatusPost.Voltage));
            parameters.AddIfNotNull("c1", StatusPost.Cumulative != Enums.CumulativeStatusType.None ? (int?)StatusPost.Cumulative : null);
            parameters.AddIfNotNull("n", StatusPost.Net ? 1 : 0);
            parameters.AddIfNotNull("v7", FormatHelper.GetValueAsString(StatusPost.ExtendedValue1));
            parameters.AddIfNotNull("v8", FormatHelper.GetValueAsString(StatusPost.ExtendedValue2));
            parameters.AddIfNotNull("v9", FormatHelper.GetValueAsString(StatusPost.ExtendedValue3));
            parameters.AddIfNotNull("v10", FormatHelper.GetValueAsString(StatusPost.ExtendedValue4));
            parameters.AddIfNotNull("v11", FormatHelper.GetValueAsString(StatusPost.ExtendedValue5));
            parameters.AddIfNotNull("v12", FormatHelper.GetValueAsString(StatusPost.ExtendedValue6));
            parameters.AddIfNotNull("m1", StatusPost.TextMessage);

            return parameters;
        }
    }
}
