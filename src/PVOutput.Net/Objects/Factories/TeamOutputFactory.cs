using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class TeamOutputFactory : IArrayStringFactory<ITeamOutput>
    {
        public IArrayStringReader<ITeamOutput> CreateArrayReader() => new CharacterDelimitedArrayStringReader<ITeamOutput>();

        public IObjectStringReader<ITeamOutput> CreateObjectReader() => new TeamOutputObjectStringReader();
    }
}
