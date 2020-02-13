using System;
using System.Globalization;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class AggregatedOutputObjectStringReader : BaseObjectStringReader<IAggregatedOutput>
    {
        public override IAggregatedOutput CreateObjectInstance() => new AggregatedOutput();

        public AggregatedOutputObjectStringReader()
        {
            var properties = new Action<IAggregatedOutput, string>[]
            {
                (t, s) => t.AggregatedDate = FormatHelper.ParseDate(s),
                (t, s) => t.Outputs = Convert.ToInt32(s),
                (t, s) => t.EnergyGenerated = Convert.ToInt32(s),
                (t, s) => t.Efficiency = Convert.ToDecimal(s, CultureInfo.CreateSpecificCulture("en-US")),
                (t, s) => t.EnergyExported = Convert.ToInt32(s),
                (t, s) => t.EnergyUsed  = Convert.ToInt32(s),
                (t, s) => t.PeakEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.OffPeakEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.ShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
