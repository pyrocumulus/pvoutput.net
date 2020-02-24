using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class DayStatisticsFactory : IStringFactory<IDayStatistics>
    {
        public IArrayStringReader<IDayStatistics> CreateArrayReader() => new CharacterDelimitedArrayStringReader<IDayStatistics>();

        public IObjectStringReader<IDayStatistics> CreateObjectReader() => new DayStatisticsObjectStringReader();
    }
}
