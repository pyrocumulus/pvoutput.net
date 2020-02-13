using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class AggregatedOutput : IAggregatedOutput
    {
        public DateTime AggregatedDate { get; set; }
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
