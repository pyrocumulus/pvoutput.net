using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class OutputPost : IOutputPost
    {
        public DateTime Date { get; set; } = DateTime.MinValue;
        public int? EnergyGenerated { get; set; }
        public int? EnergyExported { get; set; }
        public int? PeakPower { get; set; }
        public DateTime? PeakTime { get; set; }
        public string Condition { get; set; }
        public decimal? MinimumTemperature { get; set; }
        public decimal? MaximumTemperature { get; set; }
        public string Comments { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
        public int? HighShoulderEnergyImport { get; set; }
        public int? Consumption { get; set; }
    }
}
