using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class TeamFactory : IStringFactory<ITeam>
    {
        public IArrayStringReader<ITeam> CreateArrayReader() => throw new NotImplementedException();

        public IObjectStringReader<ITeam> CreateObjectReader() => new TeamObjectStringReader();
    }
}
