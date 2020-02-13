using System;

namespace PVOutput.Net.Objects
{
    public interface ITeamOutput
    {
        DateTime Date { get; set; }
        int Outputs { get; set; }
        decimal Efficiency { get; set; }
        int TotalGeneration { get; set; }
        int AverageGeneration { get; set; }
        int TotalExported { get; set; }
        int TotalConsumption { get; set; }
        int AverageConsumption { get; set; }
        int TotalImported { get; set; }
    }
}
