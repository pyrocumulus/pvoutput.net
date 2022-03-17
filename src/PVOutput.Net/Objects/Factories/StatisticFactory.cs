using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class StatisticFactory : IObjectStringFactory<IStatistic>
    {
        public IObjectStringReader<IStatistic> CreateObjectReader() => new StatisticObjectStringReader();
    }
}
