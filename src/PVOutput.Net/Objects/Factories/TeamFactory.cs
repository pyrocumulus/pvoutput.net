using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class TeamFactory : IObjectStringFactory<ITeam>
    {
        public IObjectStringReader<ITeam> CreateObjectReader() => new TeamObjectStringReader();
    }
}
