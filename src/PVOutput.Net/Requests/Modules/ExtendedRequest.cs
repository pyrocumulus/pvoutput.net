﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class ExtendedRequest : GetRequest<IExtended>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Limit { get; set; }

        public override HttpMethod Method => HttpMethod.Get;

        public override string UriTemplate => "getextended.jsp{?df,dt,limit}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["df"] = FromDate != null ? FormatHelper.GetDateAsString(FromDate.Value) : null,
            ["dt"] = ToDate != null ? FormatHelper.GetDateAsString(ToDate.Value) : null,
            ["limit"] = Limit
        };
    }
}
