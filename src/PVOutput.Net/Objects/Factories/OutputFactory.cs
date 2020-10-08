using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class OutputFactory : IArrayStringFactory<IOutput>
    {
        public IArrayStringReader<IOutput> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IOutput>();

        public IObjectStringReader<IOutput> CreateObjectReader() => new OutputObjectStringReader();
    }
}
