using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class AddOutputRequest : PostRequest
    {
        public IOutputPost Output { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addoutput.jsp{?d,g,e,pp,pt,cd,tm,tx,cm,ip,io,is,ih,c}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["d"] = FormatHelper.GetDateAsString(Output.OutputDate),
            ["g"] = Output.EnergyGenerated,
            ["e"] = Output.EnergyExported,
            ["pp"] = Output.PeakPower,
            ["pt"] = Output.PeakTime != null ? FormatHelper.GetTimeAsString(Output.PeakTime.Value) : null,
            ["cd"] = FormatHelper.GetEnumerationDescription(Output.Condition),
            ["tm"] = FormatHelper.GetValueAsString(Output.MinimumTemperature),
            ["tx"] = FormatHelper.GetValueAsString(Output.MaximumTemperature),
            ["cm"] = Output.Comments,
            ["ip"] = FormatHelper.GetValueAsString(Output.PeakEnergyImport),
            ["io"] = FormatHelper.GetValueAsString(Output.OffPeakEnergyImport),
            ["is"] = FormatHelper.GetValueAsString(Output.ShoulderEnergyImport),
            ["ih"] = FormatHelper.GetValueAsString(Output.HighShoulderEnergyImport),
            ["c"] = Output.Consumption
        };
    }
}
