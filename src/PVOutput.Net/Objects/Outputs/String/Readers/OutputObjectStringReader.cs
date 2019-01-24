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
    internal class OutputObjectStringReader : BaseObjectStringReader<IOutput>
    {
		public OutputObjectStringReader()
		{
			var properties = new Action<IOutput, string>[]
			{
				(t, s) => t.Date = FormatHelper.ParseDate(s),
				(t, s) => t.EnergyGenerated = Convert.ToInt32(s),
				(t, s) => t.Efficiency = FormatHelper.ParseNumeric(s),
				(t, s) => t.EnergyExported = Convert.ToInt32(s),
				(t, s) => t.EnergyUsed = Convert.ToInt32(s),
				(t, s) => t.PeakPower = FormatHelper.ParseValue<int>(s),
				(t, s) => t.PeakTime = s.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? (DateTime?)null : t.Date.Add(FormatHelper.ParseTime(s).TimeOfDay),
				(t, s) => t.Condition = s,
				(t, s) => t.MinimumTemperature = FormatHelper.ParseValue<int>(s),
				(t, s) => t.MaximumTemperature = FormatHelper.ParseValue<int>(s),
				(t, s) => t.PeakEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.OffPeakEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.ShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(s),
				(t, s) => t.Insolation = FormatHelper.ParseValue<int>(s)
			};

			_parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
		}

        public override IOutput CreateObjectInstance() => new Output();
    }
}
