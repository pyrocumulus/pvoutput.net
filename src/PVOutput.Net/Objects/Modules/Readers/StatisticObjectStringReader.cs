using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class StatisticObjectStringReader : BaseObjectStringReader<IStatistic>
    {
        public override IStatistic CreateObjectInstance() => new Statistic();

        public StatisticObjectStringReader()
        {
            var properties = new Action<IStatistic, string>[]
            {
                (t, s) => t.EnergyGenerated = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.EnergyExported = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.MinimumGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.MaximumGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageEfficiency = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.Outputs = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.ActualDateFrom = FormatHelper.ParseDate(s),
                (t, s) => t.ActualDateTo = FormatHelper.ParseDate(s),
                (t, s) => t.RecordEfficiency = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.RecordDate = FormatHelper.ParseDate(s),

                (t, s) => t.EnergyConsumed = FormatHelper.GetValue<int>(s),
                (t, s) => t.PeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.OffPeakEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.ShoulderEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.HighShoulderEnergyImport = FormatHelper.GetValue<int>(s),
                (t, s) => t.AverageConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.MinimumConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.MaximumConsumption = FormatHelper.GetValue<int>(s),

                (t, s) => t.CreditAmount = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.DebitAmount = FormatHelper.GetValue<decimal>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
