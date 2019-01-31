using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Status;
using PVOutput.Net.Objects.Status.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
	internal class StatusFactory : IStringFactory<IStatus>
	{
		public IArrayStringReader<IStatus> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IStatus>();

		public IObjectStringReader<IStatus> CreateObjectReader() => new StatusObjectStringReader();
	}
}
