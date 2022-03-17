using System;
using System.Globalization;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class TeamOutputObjectStringReader : BaseObjectStringReader<ITeamOutput>
    {
        public override ITeamOutput CreateObjectInstance() => new TeamOutput();

        public TeamOutputObjectStringReader()
        {
            var properties = new Action<ITeamOutput, string>[]
            {
                (t, s) => t.OutputDate = FormatHelper.ParseDate(s),
                (t, s) => t.Outputs = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Efficiency = Convert.ToDecimal(s, CultureInfo.CreateSpecificCulture("en-US")),
                (t, s) => t.TotalGeneration = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageGeneration  = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.TotalExported = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.TotalConsumption = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.AverageConsumption = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.TotalImported = FormatHelper.GetValueOrDefault<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
