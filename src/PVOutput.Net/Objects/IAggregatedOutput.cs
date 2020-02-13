using System;

namespace PVOutput.Net.Objects
{
    public interface IAggregatedOutput
    {
        DateTime AggregatedDate { get; set; }
        int Outputs { get; set; }
        int EnergyGenerated { get; set; }
        decimal Efficiency { get; set; }
        int EnergyExported { get; set; }
        int EnergyUsed { get; set; }
        int? PeakEnergyImport { get; set; }
        int? OffPeakEnergyImport { get; set; }
        int? ShoulderEnergyImport { get; set; }
        int? HighShoulderEnergyImport { get; set; }
    }
}
