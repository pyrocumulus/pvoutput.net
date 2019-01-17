using PVOutput.Net.Objects.Outputs;
using PVOutput.Net.Objects.Outputs.String.Readers;
using PVOutput.Net.Objects.String;
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
