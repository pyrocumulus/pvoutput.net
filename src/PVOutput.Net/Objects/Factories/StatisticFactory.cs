using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal class StatisticFactory : IStringFactory<IStatistic>
    {
        public IArrayStringReader<IStatistic> CreateArrayReader() => new SemiColonSeparatedArrayStringReader<IStatistic>();

        public IObjectStringReader<IStatistic> CreateObjectReader() => new StatisticObjectStringReader();
    }
}
