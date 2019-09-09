using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;
using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
    internal class AggregatedOutputFactory : IStringFactory<IAggregatedOutput>
    {
        public IArrayStringReader<IAggregatedOutput> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IAggregatedOutput>();

        public IObjectStringReader<IAggregatedOutput> CreateObjectReader() => new AggregatedOutputObjectStringReader();
    }
}
