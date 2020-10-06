using System;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class StatusObjectStringReader : BaseObjectStringReader<IStatus>
    {
        public override IStatus CreateObjectInstance() => new Implementations.Status();

        public StatusObjectStringReader()
        {
            var properties = new Action<IStatus, string>[]
            {
                (t, s) => t.Timestamp = FormatHelper.ParseDate(s),
                (t, s) => t.Timestamp = t.Timestamp.Add(FormatHelper.ParseTime(s)),
                (t, s) => t.EnergyGeneration = FormatHelper.GetValue<int>(s),
                (t, s) => t.PowerGeneration = FormatHelper.GetValue<int>(s),
                (t, s) => t.EnergyConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.PowerConsumption = FormatHelper.GetValue<int>(s),
                (t, s) => t.NormalisedOutput = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.Temperature = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.Voltage = FormatHelper.GetValue<decimal>(s),
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
