using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class SystemSearchResultObjectStringReader : BaseObjectStringReader<ISystemSearchResult>
    {
        public override ISystemSearchResult CreateObjectInstance() => new Implementations.SystemSearchResult();

        public SystemSearchResultObjectStringReader()
        {
            var properties = new Action<ISystemSearchResult, string>[]
            {
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = Convert.ToInt32(s),
                (t, s) => 
                {
                    SplitPostCode(s, out int postcode, out string country);
                    t.Postcode = postcode;
                    t.Country = country;
                },
                (t, s) => t.Orientation = s,
                (t, s) => t.NumberOfOutputs = Convert.ToInt32(s),
                (t, s) => t.LastOutput = s,
                (t, s) => t.SystemId = Convert.ToInt32(s),
                (t, s) => t.Panel = s,
                (t, s) => t.Inverter = s,
                (t, s) => t.Distance = FormatHelper.ParseValue<int>(s),
                (t, s) => t.Latitude = FormatHelper.ParseValue<double>(s),
                (t, s) => t.Longitude = FormatHelper.ParseValue<double>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }

        private void SplitPostCode(string value, out int postcode, out string country)
        {
            if (!value.Contains(" "))
            {
                postcode = Convert.ToInt32(value);
                country = null;
                return;
            }

            var parts = value.Split(' ');
            country = parts[0];
            postcode = Convert.ToInt32(parts[1]);
        }
    }
}
