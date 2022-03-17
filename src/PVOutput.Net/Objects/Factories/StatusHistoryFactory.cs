﻿using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class StatusHistoryFactory : IArrayStringFactory<IStatusHistory>
    {
        public IArrayStringReader<IStatusHistory> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IStatusHistory>();

        public IObjectStringReader<IStatusHistory> CreateObjectReader() => new StatusHistoryObjectStringReader();
    }
}
