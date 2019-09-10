using System;

namespace PVOutput.Net.Objects.Modules
{
    public interface IStatusHistory
    {
        DateTime Date { get; set; }
        int? EnergyGeneration { get; set; }
        decimal? EnergyEfficiency { get; set; }
        int? InstantaneousPower { get; set; }
        int? AveragePower { get; set; }
        decimal? NormalisedOutput { get; set; }
        int? EnergyConsumption { get; set; }
        int? PowerConsumption { get; set; }
        decimal? Temperature { get; set; }
        decimal? Volts { get; set; }
        decimal? ExtendedValue1 { get; set; }
        decimal? ExtendedValue2 { get; set; }
        decimal? ExtendedValue3 { get; set; }
        decimal? ExtendedValue4 { get; set; }
        decimal? ExtendedValue5 { get; set; }
        decimal? ExtendedValue6 { get; set; }
    }
}
