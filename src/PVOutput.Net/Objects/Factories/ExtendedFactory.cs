using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class ExtendedFactory : IArrayStringFactory<IExtended>
    {
        public IArrayStringReader<IExtended> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IExtended>();

        public IObjectStringReader<IExtended> CreateObjectReader() => new ExtendedObjectStringReader();
    }
}
