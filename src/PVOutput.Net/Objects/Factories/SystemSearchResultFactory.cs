using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class SystemSearchResultFactory : IArrayStringFactory<ISystemSearchResult>
    {
        public IArrayStringReader<ISystemSearchResult> CreateArrayReader() => new LineDelimitedArrayStringReader<ISystemSearchResult>();

        public IObjectStringReader<ISystemSearchResult> CreateObjectReader() => new SystemSearchResultObjectStringReader();
    }
}
