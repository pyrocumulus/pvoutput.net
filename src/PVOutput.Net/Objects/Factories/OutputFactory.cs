using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class OutputFactory : IStringFactory<IOutput>
    {
        public IArrayStringReader<IOutput> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IOutput>();

        public IObjectStringReader<IOutput> CreateObjectReader() => new OutputObjectStringReader();
    }
}
