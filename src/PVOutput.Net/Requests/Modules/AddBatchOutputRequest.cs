﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class AddBatchOutputRequest : PostRequest
    {
        public IEnumerable<IBatchOutputPost> Outputs { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addbatchoutput.jsp{?data}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["data"] = FormatOutputs()
        };

        private string FormatOutputs()
        {
            var sb = new StringBuilder();

            foreach (var output in Outputs)
            {
                sb.Append(FormatOutput(output)).Append(';');
            }

            return sb.ToString();
        }

        private string FormatOutput(IBatchOutputPost output)
        {
            var sb = new StringBuilder();
            sb.Append(FormatHelper.GetDateAsString(output.Date));
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

            if (output.PeakTime != null)
            {
                sb.Append(FormatHelper.GetDateAsString(output.PeakTime.Value));
                sb.Append(',');
                sb.Append(FormatHelper.GetTimeAsString(output.PeakTime.Value));
            }
            else
            {
                sb.Append(',');
            }
            sb.Append(',');

            if (output.Condition != null)
            {
                sb.Append(output.Condition);
            }
            sb.Append(',');

            if (output.MinimumTemperature != null)
            {
                sb.Append(FormatHelper.GetValueAsString(output.MinimumTemperature));
            }
            sb.Append(',');

            if (output.MinimumTemperature != null)
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
            { sb.Append(FormatHelper.GetValueAsString(output.PeakEnergyImport)); 
            }
            sb.Append(',');

            if (output.OffPeakEnergyImport != null) 
            { sb.Append(FormatHelper.GetValueAsString(output.OffPeakEnergyImport)); 
            }
            sb.Append(',');

            if (output.ShoulderEnergyImport != null) 
            { sb.Append(FormatHelper.GetValueAsString(output.ShoulderEnergyImport)); 
            }
            sb.Append(',');

            if (output.HighShoulderEnergyImport != null) 
            { sb.Append(FormatHelper.GetValueAsString(output.HighShoulderEnergyImport)); 
            }
            sb.Append(',');

            return sb.ToString();
        }
    }
}
