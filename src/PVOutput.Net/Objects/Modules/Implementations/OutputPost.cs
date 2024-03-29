﻿using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class OutputPost : IOutputPost
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
        public int? HighShoulderEnergyImport { get; set; }
        public int? Consumption { get; set; }
        public int? PeakEnergyExport { get; set; }
        public int? OffPeakEnergyExport { get; set; }
        public int? ShoulderEnergyExport { get; set; }
        public int? HighShoulderEnergyExport { get; set; }
    }
}
