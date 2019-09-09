using PVOutput.Net.Objects.Modules.Implementations;
using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class TeamOutputObjectStringReader : BaseObjectStringReader<ITeamOutput>
    {
        public override ITeamOutput CreateObjectInstance() => new TeamOutput();

		public TeamOutputObjectStringReader()
		{
			var properties = new Action<ITeamOutput, string>[]
			{
				(t, s) => t.Date = FormatHelper.ParseDate(s),
				(t, s) => t.Outputs = Convert.ToInt32(s),
				(t, s) => t.Efficiency = Convert.ToDecimal(s, CultureInfo.CreateSpecificCulture("en-US")),
				(t, s) => t.TotalGeneration = Convert.ToInt32(s),
				(t, s) => t.AverageGeneration  = Convert.ToInt32(s),
				(t, s) => t.TotalExported = Convert.ToInt32(s),
				(t, s) => t.TotalConsumption = Convert.ToInt32(s),
				(t, s) => t.AverageConsumption = Convert.ToInt32(s),
				(t, s) => t.TotalImported = Convert.ToInt32(s)
			};

			_parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
		}
	}
}
