using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class AggregatedOutputFactory : IStringFactory<IAggregatedOutput>
    {
        public IArrayStringReader<IAggregatedOutput> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IAggregatedOutput>();

        public IObjectStringReader<IAggregatedOutput> CreateObjectReader() => new AggregatedOutputObjectStringReader();
    }
}
