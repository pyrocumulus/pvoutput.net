using System;

namespace PVOutput.Net.Objects
{
    public interface IOutput
    {
        DateTime Date { get; set; }
        int EnergyGenerated { get; set; }
        decimal Efficiency { get; set; }
        int EnergyExported { get; set; }
        int EnergyUsed { get; set; }
        int? PeakPower { get; set; }
        DateTime? PeakTime { get; set; }
        string Condition { get; set; }
        int? MinimumTemperature { get; set; }
        int? MaximumTemperature { get; set; }
        int? PeakEnergyImport { get; set; }
        int? OffPeakEnergyImport { get; set; }
        int? ShoulderEnergyImport { get; set; }
        int? HighShoulderEnergyImport { get; set; }
        int? Insolation { get; set; }
    }
}
