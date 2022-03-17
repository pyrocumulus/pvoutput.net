using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class FavouriteFactory : IArrayStringFactory<IFavourite>
    {
        public IArrayStringReader<IFavourite> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IFavourite>('\n');

        public IObjectStringReader<IFavourite> CreateObjectReader() => new FavouriteObjectStringReader();
    }
}
