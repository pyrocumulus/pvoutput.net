using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Outputs;

namespace PVOutput.Net.Objects.Outputs.Implementations
{
    internal class AggregatedOutput : IAggregatedOutput
    {
        public DateTime Date { get; set; }
        public int Outputs { get; set; }
        public int EnergyGenerated { get; set; }
        public decimal Efficiency { get; set; }
        public int EnergyExported { get; set; }
        public int EnergyUsed { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
        public int? HighShoulderEnergyImport { get; set; }
    }
}
