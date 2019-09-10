using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class StatusFactory : IStringFactory<IStatus>
    {
        public IArrayStringReader<IStatus> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IStatus>();

        public IObjectStringReader<IStatus> CreateObjectReader() => new StatusObjectStringReader();
    }
}
