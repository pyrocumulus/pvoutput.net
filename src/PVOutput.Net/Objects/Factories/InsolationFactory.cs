using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class InsolationFactory : IArrayStringFactory<IInsolation>
    {
        public IArrayStringReader<IInsolation> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IInsolation>();

        public IObjectStringReader<IInsolation> CreateObjectReader() => new InsolationObjectStringReader();
    }
}
