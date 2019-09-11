using System;
using System.Globalization;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class TeamObjectStringReader : BaseObjectStringReader<ITeam>
    {
        public override ITeam CreateObjectInstance() => new Team();

        protected override char ItemDelimiter => ';';

        public TeamObjectStringReader()
        {
            var properties = new Action<ITeam, string>[]
            {
                (t, s) => t.Name = s,
                (t, s) => t.TeamSize = Convert.ToInt32(s),
                (t, s) => t.AverageSize = Convert.ToInt32(s),
                (t, s) => t.NumberOfSystems = Convert.ToInt32(s),
                (t, s) => t.EnergyGenerated  = Convert.ToInt64(s),
                (t, s) => t.Outputs = Convert.ToInt32(s),
                (t, s) => t.EnergyAverage = Convert.ToInt32(s),
                (t, s) => t.Type = s,
                (t, s) => t.Description = s,
                (t, s) => t.CreationDate = FormatHelper.ParseDate(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
