using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class ExtendedFactory : IStringFactory<IExtended>
    {
        public IArrayStringReader<IExtended> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IExtended>();

        public IObjectStringReader<IExtended> CreateObjectReader() => new ExtendedObjectStringReader();
    }
}
