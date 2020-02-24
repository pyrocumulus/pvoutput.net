using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class Status : IStatus
    {
        public DateTime Timestamp { get; set; }
        public int? EnergyGeneration { get; set; }
        public int? PowerGeneration { get; set; }
        public int? EnergyConsumption { get; set; }
        public int? PowerConsumption { get; set; }
        public decimal? NormalisedOutput { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Voltage { get; set; }
        public decimal? ExtendedValue1 { get; set; }
        public decimal? ExtendedValue2 { get; set; }
        public decimal? ExtendedValue3 { get; set; }
        public decimal? ExtendedValue4 { get; set; }
        public decimal? ExtendedValue5 { get; set; }
        public decimal? ExtendedValue6 { get; set; }
    }
}
