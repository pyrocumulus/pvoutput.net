using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
	internal class MissingFactory : IStringFactory<IMissing>
	{
		public IArrayStringReader<IMissing> CreateArrayReader() => throw new NotImplementedException();

		public IObjectStringReader<IMissing> CreateObjectReader() => new MissingObjectStringReader();
	}
}
