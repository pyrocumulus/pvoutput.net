using PVOutput.Net.Objects.Outputs;
using PVOutput.Net.Objects.Outputs.String.Readers;
using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
    internal class TeamOutputFactory : IStringFactory<ITeamOutput>
    {
        public IArrayStringReader<ITeamOutput> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<ITeamOutput>();

        public IObjectStringReader<ITeamOutput> CreateObjectReader() => new TeamOutputObjectStringReader();
    }
}
