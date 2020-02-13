using System;

namespace PVOutput.Net.Objects
{
    public interface IBatchOutputPost
    {
        DateTime OutputDate { get; set; }
        int? EnergyGenerated { get; set; }
        int? EnergyExported { get; set; }
        int? PeakPower { get; set; }
        DateTime? PeakTime { get; set; }
        string Condition { get; set; }
        decimal? MinimumTemperature { get; set; }
        decimal? MaximumTemperature { get; set; }
        string Comments { get; set; }
        int? PeakEnergyImport { get; set; }
        int? OffPeakEnergyImport { get; set; }
        int? ShoulderEnergyImport { get; set; }
        int? HighShoulderEnergyImport { get; set; }
    }
}
