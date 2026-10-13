using System;
using System.Collections.Generic;
using System.Net.Http;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class AddOutputRequest : PostRequest
    {
        public required IOutputPost Output { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addoutput.jsp{?d,g,e,pp,pt,cd,tm,tx,cm,ip,io,is,ih,c,ep,eo,es,eh}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>();

            parameters.AddIfNotNull("d", FormatHelper.GetDateAsString(Output.OutputDate));
            parameters.AddIfNotNull("g", Output.EnergyGenerated);
            parameters.AddIfNotNull("e", Output.EnergyExported);
            parameters.AddIfNotNull("pp", Output.PeakPower);
            parameters.AddIfNotNull("pt", Output.PeakTime != null ? FormatHelper.GetTimeAsString(Output.PeakTime.Value) : null);
            parameters.AddIfNotNull("cd", FormatHelper.GetEnumerationDescription(Output.Condition));
            parameters.AddIfNotNull("tm", FormatHelper.GetValueAsString(Output.MinimumTemperature));
            parameters.AddIfNotNull("tx", FormatHelper.GetValueAsString(Output.MaximumTemperature));
            parameters.AddIfNotNull("cm", Output.Comments);
            parameters.AddIfNotNull("ip", FormatHelper.GetValueAsString(Output.PeakEnergyImport));
            parameters.AddIfNotNull("io", FormatHelper.GetValueAsString(Output.OffPeakEnergyImport));
            parameters.AddIfNotNull("is", FormatHelper.GetValueAsString(Output.ShoulderEnergyImport));
            parameters.AddIfNotNull("ih", FormatHelper.GetValueAsString(Output.HighShoulderEnergyImport));
            parameters.AddIfNotNull("c", Output.Consumption);
            parameters.AddIfNotNull("ep", FormatHelper.GetValueAsString(Output.PeakEnergyExport));
            parameters.AddIfNotNull("eo", FormatHelper.GetValueAsString(Output.OffPeakEnergyExport));
            parameters.AddIfNotNull("es", FormatHelper.GetValueAsString(Output.ShoulderEnergyExport));
            parameters.AddIfNotNull("eh", FormatHelper.GetValueAsString(Output.HighShoulderEnergyExport));

            return parameters;
        }
    }
}
