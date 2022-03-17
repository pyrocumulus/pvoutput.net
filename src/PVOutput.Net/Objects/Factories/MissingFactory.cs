using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class MissingFactory : IObjectStringFactory<IMissing>
    {
        public IObjectStringReader<IMissing> CreateObjectReader() => new MissingObjectStringReader();
    }
}
