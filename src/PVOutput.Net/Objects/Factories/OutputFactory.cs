using PVOutput.Net.Objects.Outputs;
using PVOutput.Net.Objects.Outputs.String.Readers;
using PVOutput.Net.Objects.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Factories
{
    internal class OutputFactory : IStringFactory<IOutput>
    {
        public IArrayStringReader<IOutput> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IOutput>();

        public IObjectStringReader<IOutput> CreateObjectReader() => new OutputObjectStringReader();
    }
}
