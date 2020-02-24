using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class DayStatistics : IDayStatistics
    {
        public int EnergyGeneration { get; set; }
        public int PowerGeneration { get; set; }
        public int PeakPower { get; set; }
        public DateTime PeakTime { get; set; }
        public int? EnergyConsumption { get; set; }
        public int? PowerConsumption { get; set; }
        public int? StandbyPower { get; set; }
        public DateTime? StandbyPowerTime { get; set; }
        public decimal? MinimumTemperature { get; set; }
        public decimal? MaximumTemperature { get; set; }
        public decimal? AverageTemperature { get; set; }
    }
}
