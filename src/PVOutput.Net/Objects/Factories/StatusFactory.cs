using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class StatusFactory : IObjectStringFactory<IStatus>
    {
        public IObjectStringReader<IStatus> CreateObjectReader() => new StatusObjectStringReader();
    }
}
