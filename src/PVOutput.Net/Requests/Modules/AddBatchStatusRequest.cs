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
    internal sealed class AddBatchStatusRequest : PostRequest
    {
        public IEnumerable<IBatchStatusPost> StatusPosts { get; set; }

        public bool Cumulative { get; set; }

        public override HttpMethod Method => HttpMethod.Post;

        public override string UriTemplate => "addbatchstatus.jsp{?c1,n,data}";

        public override IDictionary<string, object> GetUriPathParameters() => new Dictionary<string, object>
        {
            ["c1"] = Cumulative ? 1 : 0,
            ["n"] = 0,
            ["data"] = FormatStatusPosts()
        };

        private string FormatStatusPosts()
        {
            var sb = new StringBuilder();

            foreach (IBatchStatusPost status in StatusPosts)
            {
                sb.Append(FormatStatusPost(status)).Append(';');
            }

            return sb.ToString();
        }

        internal static string FormatStatusPost(IBatchStatusPost status)
        {
            var sb = new StringBuilder();
            sb.Append(FormatHelper.GetDateAsString(status.Timestamp));
            sb.Append(',');
            sb.Append(FormatHelper.GetTimeAsString(status.Timestamp));
            sb.Append(',');

            sb.Append(status.EnergyGeneration);
            sb.Append(',');

            if (status.PowerGeneration != null)
            {
                sb.Append(status.PowerGeneration);
            }
            sb.Append(',');

            if (status.EnergyConsumption != null)
            {
                sb.Append(status.EnergyConsumption);
            }
            sb.Append(',');

            if (status.PowerConsumption != null)
            {
                sb.Append(status.PowerConsumption);
            }
            sb.Append(',');

            if (status.Temperature != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.Temperature));
            }
            sb.Append(',');

            if (status.Voltage != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.Voltage));
            }
            sb.Append(',');

            if (status.ExtendedValue1 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue1));
            }
            sb.Append(',');

            if (status.ExtendedValue2 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue2));
            }
            sb.Append(',');

            if (status.ExtendedValue3 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue3));
            }
            sb.Append(',');

            if (status.ExtendedValue4 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue4));
            }
            sb.Append(',');

            if (status.ExtendedValue5 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue5));
            }
            sb.Append(',');

            if (status.ExtendedValue6 != null)
            {
                sb.Append(FormatHelper.GetValueAsString(status.ExtendedValue6));
            }

            return sb.ToString();
        }
    }
}
