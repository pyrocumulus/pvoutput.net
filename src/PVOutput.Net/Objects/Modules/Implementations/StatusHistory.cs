using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    public class StatusHistory : IStatusHistory
    {
        public DateTime Date { get; set; }
        public int? EnergyGeneration { get; set; }
        public decimal? EnergyEfficiency { get; set; }
        public int? InstantaneousPower { get; set; }
        public int? AveragePower { get; set; }
        public decimal? NormalisedOutput { get; set; }
        public int? EnergyConsumption { get; set; }
        public int? PowerConsumption { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Volts { get; set; }
        public decimal? ExtendedValue1 { get; set; }
        public decimal? ExtendedValue2 { get; set; }
        public decimal? ExtendedValue3 { get; set; }
        public decimal? ExtendedValue4 { get; set; }
        public decimal? ExtendedValue5 { get; set; }
        public decimal? ExtendedValue6 { get; set; }
    }
}
