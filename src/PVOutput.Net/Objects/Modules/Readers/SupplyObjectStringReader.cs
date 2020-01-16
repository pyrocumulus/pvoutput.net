using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class SupplyObjectStringReader : BaseObjectStringReader<ISupply>
    {
        public override ISupply CreateObjectInstance() => new Implementations.Supply();

        public SupplyObjectStringReader()
        {
            var properties = new Action<ISupply, string>[]
            {
                (t, s) => t.Timestamp = FormatHelper.ParseTimeStamp(s),
                (t, s) => t.RegionName = s,
                (t, s) => t.Utilisation = FormatHelper.ParseValueWithDefault<decimal>(s),
                (t, s) => t.TotalPowerOutput = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.TotalPowerInput = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.AveragePowerOutput = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.AveragePowerInput = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.AverageNetPower = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.SystemsOut = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.SystemsIn = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.TotalSize = FormatHelper.ParseValueWithDefault<int>(s),
                (t, s) => t.AverageSize = FormatHelper.ParseValueWithDefault<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
