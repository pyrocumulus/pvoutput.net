using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Readers
{
	internal class MissingObjectStringReader : BaseObjectStringReader<IMissing>
	{
		public override IMissing CreateObjectInstance() => new Implementations.Missing();

		public MissingObjectStringReader()
		{
			_parsers.Add(ParseMissingDates);
		}

		private void ParseMissingDates(IMissing target, TextReader reader)
		{
			var dates = ReadPropertiesForGroup(reader);

			if (dates.Count == 0)
			{
				return;
			}

			var result = new List<DateTime>();
			foreach (string date in dates)
			{
				result.Add(FormatHelper.ParseDate(date));
			}
			target.Dates = result;
		}

		protected override IMissing GetDefaultResult()
		{
			var missing = CreateObjectInstance();
			missing.Dates = new List<DateTime>();
			return missing;
		}
	}
}
