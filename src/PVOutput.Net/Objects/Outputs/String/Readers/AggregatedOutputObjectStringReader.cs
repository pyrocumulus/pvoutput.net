using PVOutput.Net.Objects.Outputs.Implementations;
using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Outputs.String.Readers
{
    internal class AggregatedOutputObjectStringReader : BaseObjectStringReader<IAggregatedOutput>
    {
		public override IAggregatedOutput CreateObjectInstance() => new AggregatedOutput();

		public AggregatedOutputObjectStringReader()
		{
			var properties = new Action<IAggregatedOutput, string>[]
			{
				(t, s) => t.Date = FormatHelper.ParseDate(s),
				(t, s) => t.Outputs = Convert.ToInt32(s),
				(t, s) => t.EnergyGenerated = Convert.ToInt32(s),
				(t, s) => t.Efficiency = Convert.ToDecimal(s, CultureInfo.CreateSpecificCulture("en-US")),
				(t, s) => t.EnergyExported = Convert.ToInt32(s),
				(t, s) => t.EnergyUsed  = Convert.ToInt32(s),
				(t, s) => t.PeakEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.OffPeakEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.ShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(s)
			};

			_parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
		}
    }
}
