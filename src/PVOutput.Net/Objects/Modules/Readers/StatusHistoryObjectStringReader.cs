using System;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class StatusHistoryObjectStringReader : BaseObjectStringReader<IStatusHistory>
    {
        public override IStatusHistory CreateObjectInstance() => new Implementations.StatusHistory();

        public StatusHistoryObjectStringReader()
        {
            var properties = new Action<IStatusHistory, string>[]
            {
                (t, s) => t.StatusDate = FormatHelper.ParseDate(s),
                (t, s) => t.StatusDate = s.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? t.StatusDate : t.StatusDate.Add(FormatHelper.ParseTime(s).TimeOfDay),
                (t, s) => t.EnergyGeneration = FormatHelper.GetValue<int>(s),
                (t, s) => t.EnergyEfficiency = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.InstantaneousPower = FormatHelper.GetValue<int>(s),
                (t, s) => t.AveragePower = FormatHelper.GetValue<int>(s),
                (t, s) => t.NormalisedOutput = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.EnergyConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.PowerConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.Temperature = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.Volts = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue1 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue2 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue3 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue4 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue5 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue6 = FormatHelper.GetValue<decimal>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
