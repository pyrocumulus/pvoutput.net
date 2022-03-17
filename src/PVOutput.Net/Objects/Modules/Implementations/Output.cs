using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class Output : IOutput
    {
        public DateTime OutputDate { get; set; }
        public int EnergyGenerated { get; set; }
        public decimal Efficiency { get; set; }
        public int EnergyExported { get; set; }
        public int EnergyUsed { get; set; }
        public int? PeakPower { get; set; }
        public TimeSpan? PeakTime { get; set; }
        public WeatherCondition Condition { get; set; }
        public int? MinimumTemperature { get; set; }
        public int? MaximumTemperature { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
        public int? HighShoulderEnergyImport { get; set; }
        public int? Insolation { get; set; }
    }
}
