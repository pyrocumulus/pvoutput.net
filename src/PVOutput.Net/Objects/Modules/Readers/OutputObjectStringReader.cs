using System;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class OutputObjectStringReader : BaseObjectStringReader<IOutput>
    {
        public OutputObjectStringReader()
        {
            var properties = new Action<IOutput, string>[]
            {
                (t, s) => t.OutputDate = FormatHelper.ParseDate(s),
                (t, s) => t.EnergyGenerated = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Efficiency = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.EnergyExported = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.EnergyUsed = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PeakPower = FormatHelper.GetValue<int>(s),
                (t, s) => t.PeakTime = s.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? (TimeSpan?)null : FormatHelper.ParseTime(s),
                (t, s) => t.Condition = FormatHelper.DescriptionToEnumValue<WeatherCondition>(s),
                (t, s) => t.MinimumTemperature = FormatHelper.GetValue<int>(s),
                (t, s) => t.MaximumTemperature = FormatHelper.GetValue<int>(s),
                (t, s) => t.PeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.OffPeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.ShoulderEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.HighShoulderEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.Insolation = FormatHelper.GetValue<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }

        public override IOutput CreateObjectInstance() => new Output();
    }
}
