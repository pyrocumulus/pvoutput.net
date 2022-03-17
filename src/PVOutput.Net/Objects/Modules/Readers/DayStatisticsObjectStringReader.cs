using System;
using System.Collections.Generic;
using System.IO;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class DayStatisticsObjectStringReader : BaseObjectStringReader<IDayStatistics>
    {
        public override IDayStatistics CreateObjectInstance() => new Implementations.DayStatistics();

        public DayStatisticsObjectStringReader()
        {
            _parsers.Add(ParseGenerationProperties);
            _parsers.Add(ParseConsumptionProperties);
            _parsers.Add(ParseTemperatureProperties);
        }

        private void ParseGenerationProperties(IDayStatistics target, TextReader reader)
        {
            var properties = new Action<IDayStatistics, string>[]
            {
                (t, s) => t.EnergyGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PowerGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PeakPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PeakTime = FormatHelper.ParseTime(s)
            };

            ParsePropertyArray(target, reader, properties);
        }

        private void ParseConsumptionProperties(IDayStatistics target, TextReader reader)
        {
            IList<string> properties = ReadPropertiesForGroup(reader);

            if (properties.Count == 0 || string.IsNullOrWhiteSpace(properties[0]))
            {
                return;
            }

            target.EnergyConsumption = FormatHelper.GetValue<int>(properties[0]);
            target.PowerConsumption = FormatHelper.GetValue<int>(properties[1]);
            target.StandbyPower = FormatHelper.GetValue<int>(properties[2]);
            target.StandbyPowerTime = FormatHelper.ParseTime(properties[3]);
        }

        private void ParseTemperatureProperties(IDayStatistics target, TextReader reader)
        {
            IList<string> properties = ReadPropertiesForGroup(reader);
            if (properties.Count == 0 || string.IsNullOrWhiteSpace(properties[0]))
            {
                return;
            }

            target.MinimumTemperature = FormatHelper.GetValue<decimal>(properties[0]);
            target.MaximumTemperature = FormatHelper.GetValue<decimal>(properties[1]);
            target.AverageTemperature = FormatHelper.GetValue<decimal>(properties[2]);
        }
    }
}
