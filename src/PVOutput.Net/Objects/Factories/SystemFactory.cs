using PVOutput.Net.Objects.String;
using PVOutput.Net.Objects.Systems;
using PVOutput.Net.Objects.Systems.String.Readers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
    internal class SystemFactory : IStringFactory<ISystem>
    {
        public IArrayStringReader<ISystem> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<ISystem>();

        public IObjectStringReader<ISystem> CreateObjectReader() => new SystemObjectStringReader();
    }
}
