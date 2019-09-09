using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
	internal class Missing : IMissing
	{
		public IEnumerable<DateTime> Dates { get; set; }
	}
}
