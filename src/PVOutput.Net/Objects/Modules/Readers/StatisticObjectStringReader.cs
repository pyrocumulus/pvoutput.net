using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class StatisticObjectStringReader : BaseObjectStringReader<IStatistic>
    {
        public override IStatistic CreateObjectInstance() => new Statistic();

        // 10051596,4365732,9031,0,25473,2.189,1113,20160822,20190908,6.175,20180701,7667511,5675645,0,0,0,10969,697,30851
        public StatisticObjectStringReader()
        {
            var properties = new Action<IStatistic, string>[]
            {
                (t, s) => t.EnergyGenerated = Convert.ToInt32(s),
                (t, s) => t.EnergyExported = Convert.ToInt32(s),
                (t, s) => t.AverageGeneration = Convert.ToInt32(s),
                (t, s) => t.MinimumGeneration = Convert.ToInt32(s),
                (t, s) => t.MaximumGeneration = Convert.ToInt32(s),
                (t, s) => t.AverageEfficiency = FormatHelper.ParseValueWithDefault<decimal>(s),
                (t, s) => t.Outputs = Convert.ToInt32(s),
                (t, s) => t.ActualDateFrom = FormatHelper.ParseDate(s),
                (t, s) => t.ActualDateTo = FormatHelper.ParseDate(s),
                (t, s) => t.RecordEfficiency = FormatHelper.ParseValueWithDefault<decimal>(s),
                (t, s) => t.RecordDate = FormatHelper.ParseDate(s),

                (t, s) => t.EnergyConsumed = FormatHelper.ParseValue<int>(s),
                (t, s) => t.PeakEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.OffPeakEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.ShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
                (t, s) => t.AverageConsumption = FormatHelper.ParseValue<int>(s),
                (t, s) => t.MinimumConsumption = FormatHelper.ParseValue<int>(s),
                (t, s) => t.MaximumConsumption = FormatHelper.ParseValue<int>(s),

                (t, s) => t.CreditAmount = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.DebitAmount = FormatHelper.ParseValue<decimal>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
