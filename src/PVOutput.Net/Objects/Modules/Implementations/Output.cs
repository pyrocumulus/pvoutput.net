using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Modules;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class Output : IOutput
    {
        public DateTime Date { get; set; }
        public int EnergyGenerated { get; set; }
        public decimal Efficiency { get; set; }
        public int EnergyExported { get; set; }
        public int EnergyUsed { get; set; }
        public int? PeakPower { get; set; }
        public DateTime? PeakTime { get; set; }
        public string Condition { get; set; }
        public int? MinimumTemperature { get; set; }
        public int? MaximumTemperature { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
        public int? HighShoulderEnergyImport { get; set; }
        public int? Insolation { get; set; }
    }
}
