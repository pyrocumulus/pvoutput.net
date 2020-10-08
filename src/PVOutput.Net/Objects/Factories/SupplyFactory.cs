using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class SupplyFactory : IArrayStringFactory<ISupply>
    {
        public IArrayStringReader<ISupply> CreateArrayReader() => new CharacterDelimitedArrayStringReader<ISupply>();

        public IObjectStringReader<ISupply> CreateObjectReader() => new SupplyObjectStringReader();
    }
}
