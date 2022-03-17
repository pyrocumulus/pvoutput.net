using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class AddOutputsRequest : PostRequest
    {
        public IEnumerable<IOutputPost> Outputs { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addoutput.jsp{?data}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["data"] = FormatOutputs()
        };

        private string FormatOutputs()
        {
            var sb = new StringBuilder();

            foreach (IOutputPost output in Outputs)
            {
                sb.Append(FormatOutputPost(output)).Append(';');
            }

            return sb.ToString();
        }

        internal static string FormatOutputPost(IOutputPost output)
        {
            var sb = new StringBuilder();
            sb.Append(FormatHelper.GetDateAsString(output.OutputDate));
            sb.Append(',');

            if (output.EnergyGenerated != null)
            {
                sb.Append(output.EnergyGenerated);
            }
            sb.Append(',');

            if (output.EnergyExported != null)
            {
                sb.Append(output.EnergyExported);
            }
            sb.Append(',');

            if (output.PeakPower != null)
            {
                sb.Append(output.PeakPower);
            }
            sb.Append(',');

            if (output.PeakTime != null)
            {
                sb.Append(FormatHelper.GetTimeAsString(output.PeakTime.Value));
            }
            sb.Append(',');

            if (output.Condition != WeatherCondition.NotSure)
            {
                sb.Append(FormatHelper.GetEnumerationDescription(output.Condition));
            }
            sb.Append(',');

            if (output.MinimumTemperature != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.MinimumTemperature));
            }
            sb.Append(',');

            if (output.MaximumTemperature != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.MaximumTemperature));
            }
            sb.Append(',');

            if (output.Comments != null)
            {
                sb.Append(output.Comments);
            }
            sb.Append(',');

            if (output.PeakEnergyImport != null) 
            { 
                sb.Append(FormatHelper.GetValueAsString(output.PeakEnergyImport)); 
            }
            sb.Append(',');

            if (output.OffPeakEnergyImport != null) 
            { 
                sb.Append(FormatHelper.GetValueAsString(output.OffPeakEnergyImport)); 
            }
            sb.Append(',');

            if (output.ShoulderEnergyImport != null) 
            { 
                sb.Append(FormatHelper.GetValueAsString(output.ShoulderEnergyImport)); 
            }
            sb.Append(',');

            if (output.HighShoulderEnergyImport != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.HighShoulderEnergyImport));
            }
            sb.Append(',');

            if (output.Consumption != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.Consumption));
            }
            sb.Append(',');

            if (output.PeakEnergyExport != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.PeakEnergyExport));
            }
            sb.Append(',');

            if (output.OffPeakEnergyExport != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.OffPeakEnergyExport));
            }
            sb.Append(',');

            if (output.ShoulderEnergyExport != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.ShoulderEnergyExport));
            }
            sb.Append(',');

            if (output.HighShoulderEnergyExport!= null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.HighShoulderEnergyExport));
            }
            return sb.ToString();
        }
    }
}
