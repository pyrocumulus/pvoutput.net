using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class Supply : ISupply
    {
        public DateTimeOffset Timestamp { get; set; }
        public string RegionName { get; set; }
        public decimal Utilisation { get; set; }
        public int TotalPowerOutput { get; set; }
        public int TotalPowerInput { get; set; }
        public int AveragePowerOutput { get; set; }
        public int AveragePowerInput { get; set; }
        public int AverageNetPower { get; set; }
        public int SystemsOut { get; set; }
        public int SystemsIn { get; set; }
        public int TotalSize { get; set; }
        public int AverageSize { get; set; }
    }
}
