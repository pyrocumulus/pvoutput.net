﻿using System;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Objects.Factories
{
    internal sealed class DayStatisticsFactory : IObjectStringFactory<IDayStatistics>
    {
        public IObjectStringReader<IDayStatistics> CreateObjectReader() => new DayStatisticsObjectStringReader();
    }
}
