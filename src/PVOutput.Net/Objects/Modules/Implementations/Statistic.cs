using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class Statistic : IStatistic
    {
        public int EnergyGenerated { get; set; }
        public int EnergyExported { get; set; }
        public int AverageGeneration { get; set; }
        public int MinimumGeneration { get; set; }
        public int MaximumGeneration { get; set; }
        public decimal AverageEfficiency { get; set; }
        public int Outputs { get; set; }
        public DateTime ActualDateFrom { get; set; }
        public DateTime ActualDateTo { get; set; }
        public decimal RecordEfficiency { get; set; }
        public DateTime RecordDate { get; set; }
        public int? EnergyConsumed { get; set; }
        public int? PeakEnergyImport { get; set; }
        public int? OffPeakEnergyImport { get; set; }
        public int? ShoulderEnergyImport { get; set; }
        public int? HighShoulderEnergyImport { get; set; }
        public int? AverageConsumption { get; set; }
        public int? MinimumConsumption { get; set; }
        public int? MaximumConsumption { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
    }
}
