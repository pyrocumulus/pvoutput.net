using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class BatchStatusPostResultFactory : IStringFactory<IBatchStatusPostResult>
    {
        public IArrayStringReader<IBatchStatusPostResult> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IBatchStatusPostResult>();

        public IObjectStringReader<IBatchStatusPostResult> CreateObjectReader() => new BatchStatusPostResultStringReader();
    }
}
