using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class SupplyObjectStringReader : BaseObjectStringReader<ISupply>
    {
        public override ISupply CreateObjectInstance() => new Implementations.Supply();

        public SupplyObjectStringReader()
        {
            var properties = new Action<ISupply, string>[]
            {
                (t, s) => t.Timestamp = FormatHelper.ParseTimeStamp(s),
                (t, s) => t.RegionName = s,
                (t, s) => t.Utilisation = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.TotalPowerOutput = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.TotalPowerInput = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AveragePowerOutput = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AveragePowerInput = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageNetPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.SystemsOut = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.SystemsIn = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.TotalSize = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageSize = FormatHelper.GetValueOrDefault<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
