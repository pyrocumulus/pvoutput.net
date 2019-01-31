using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Status.Readers
{
	internal class StatusObjectStringReader : BaseObjectStringReader<IStatus>
	{
		public override IStatus CreateObjectInstance() => new Implementations.Status();

		public StatusObjectStringReader()
		{
			var properties = new Action<IStatus, string>[]
			{
				(t, s) => t.Date = FormatHelper.ParseDate(s),
				(t, s) => t.Date = s.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? t.Date : t.Date.Add(FormatHelper.ParseTime(s).TimeOfDay),
				(t, s) => t.EnergyGeneration = FormatHelper.ParseValue<int>(s),
				(t, s) => t.PowerGeneration = FormatHelper.ParseValue<int>(s),
				(t, s) => t.EnergyConsumption = FormatHelper.ParseValue<int>(s),
				(t, s) => t.PowerConsumption = FormatHelper.ParseValue<int>(s),
				(t, s) => t.NormalisedOutput = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.Temperature = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.Volts = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue1 = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue2 = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue3 = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue4 = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue5 = FormatHelper.ParseValue<decimal>(s),
				(t, s) => t.ExtendedValue6 = FormatHelper.ParseValue<decimal>(s)
			};

			_parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
		}
	}
}
