using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class SystemSearchResultObjectStringReader : BaseObjectStringReader<ISystemSearchResult>
    {
        internal static Regex PostcodeParsingRegex = new Regex(@"(?'country'\D*?)(?'postcode'\d*)$", RegexOptions.Compiled | RegexOptions.Singleline);

        public override ISystemSearchResult CreateObjectInstance() => new Implementations.SystemSearchResult();

        public SystemSearchResultObjectStringReader()
        {
            var properties = new Action<ISystemSearchResult, string>[]
            {
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) =>
                {
                    SplitPostCode(s, out int postcode, out string country);
                    t.Postcode = postcode;
                    t.Country = country;
                },
                (t, s) => t.Orientation = FormatHelper.DescriptionToEnumValue<Orientation>(s),
                (t, s) => t.NumberOfOutputs = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.LastOutput = s,
                (t, s) => t.SystemId = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Panel = s,
                (t, s) => t.Inverter = s,
                (t, s) => t.Distance = FormatHelper.GetValue<int>(s),
                (t, s) => t.Location = new PVCoordinate(FormatHelper.GetValueOrDefault<decimal>(s), 0), // Latitude
                (t, s) => t.Location = new PVCoordinate(t.Location.Latitude, FormatHelper.GetValueOrDefault<decimal>(s)) // Add longitude
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }

        internal static void SplitPostCode(string value, out int postcode, out string country)
        {
            Match match = PostcodeParsingRegex.Match(value);

            country = "";
            postcode = 0;
            if (!string.IsNullOrEmpty(match.Groups["country"].Value))
            {
                country = match.Groups["country"].Value.Trim();
            }

            if (!string.IsNullOrEmpty(match.Groups["postcode"].Value))
            {
                postcode = FormatHelper.GetValueOrDefault<int>(match.Groups["postcode"].Value.Trim());
            }
        }
    }
}
