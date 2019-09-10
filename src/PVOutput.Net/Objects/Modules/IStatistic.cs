using System;

namespace PVOutput.Net.Objects.Modules
{
    public interface IStatistic
    {
        int EnergyGenerated { get; set; }
        int EnergyExported { get; set; }
        int AverageGeneration { get; set; }
        int MinimumGeneration { get; set; }
        int MaximumGeneration { get; set; }
        decimal AverageEfficiency { get; set; }
        int Outputs { get; set; }
        DateTime ActualDateFrom { get; set; }
        DateTime ActualDateTo { get; set; }

        decimal RecordEfficiency { get; set; }
        DateTime RecordDate { get; set; }

        // Only when includeconsumptionimport is true
        int? EnergyConsumed { get; set; }

        int? PeakEnergyImport { get; set; }
        int? OffPeakEnergyImport { get; set; }
        int? ShoulderEnergyImport { get; set; }
        int? HighShoulderEnergyImport { get; set; }

        int? AverageConsumption { get; set; }
        int? MinimumConsumption { get; set; }
        int? MaximumConsumption { get; set; }

        // Only when includecreditdebit is true
        decimal? CreditAmount { get; set; }
        decimal? DebitAmount { get; set; }
    }
}
