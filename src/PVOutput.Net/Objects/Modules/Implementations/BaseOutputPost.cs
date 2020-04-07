using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal abstract class BaseOutputPost : IBaseOutputPost
    {
        public DateTime OutputDate { get; set; } = DateTime.MinValue;
        public int? EnergyGenerated { get; set; }
        public int? EnergyExported { get; set; }
        public int? PeakPower { get; set; }
        public TimeSpan? PeakTime { get; set; }
        public WeatherCondition Condition { get; set; }
        public decimal? MinimumTemperature { get; set; }
        public decimal? MaximumTemperature { get; set; }
        public string Comments { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
    }
}
