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
                (t, s) => t.Outputs = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.EnergyGenerated = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Efficiency = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.EnergyExported = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.EnergyUsed  = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.OffPeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.ShoulderEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.HighShoulderEnergyImport = FormatHelper.GetValue<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
