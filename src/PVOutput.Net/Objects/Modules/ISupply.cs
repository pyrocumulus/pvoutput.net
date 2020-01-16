using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules
{
    public interface ISupply
    {
        DateTimeOffset Timestamp { get; set; }
        string RegionName { get; set; }
        decimal Utilisation { get; set; }
        int TotalPowerOutput { get; set; }
        int TotalPowerInput { get; set; }
        int AveragePowerOutput { get; set; }
        int AveragePowerInput { get; set; }
        int AverageNetPower { get; set; }
        int SystemsOut { get; set; }
        int SystemsIn { get; set; }
        int TotalSize { get; set; }
        int AverageSize { get; set; }
    }
}
