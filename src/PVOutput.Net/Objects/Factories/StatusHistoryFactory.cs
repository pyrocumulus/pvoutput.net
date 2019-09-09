using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
	internal class StatusHistoryFactory : IStringFactory<IStatusHistory>
	{
		public IArrayStringReader<IStatusHistory> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IStatusHistory>();

		public IObjectStringReader<IStatusHistory> CreateObjectReader() => new StatusHistoryObjectStringReader();
	}
}
