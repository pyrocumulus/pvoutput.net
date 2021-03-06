﻿using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class SystemFactory : IObjectStringFactory<ISystem>
    {
        public IObjectStringReader<ISystem> CreateObjectReader() => new SystemObjectStringReader();
    }
}
