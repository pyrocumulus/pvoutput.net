using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Missing
{
	public interface IMissing
	{
		IEnumerable<DateTime> Dates { get; set; }
	}
}
