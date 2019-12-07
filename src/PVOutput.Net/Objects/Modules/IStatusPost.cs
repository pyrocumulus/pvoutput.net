using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules
{
    public interface IStatusPost
    {
        DateTime Date { get; set; }
        int? EnergyGeneration { get; set; }
        int? PowerGeneration { get; set; }
        int? EnergyConsumption { get; set; }
        int? PowerConsumption { get; set; }
        decimal? Temperature { get; set; }
        decimal? Voltage { get; set; }
        CumulativeStatusType Cumulative { get; set; }
        bool Net { get; set; }
        decimal? ExtendedValue1 { get; set; }
        decimal? ExtendedValue2 { get; set; }
        decimal? ExtendedValue3 { get; set; }
        decimal? ExtendedValue4 { get; set; }
        decimal? ExtendedValue5 { get; set; }
        decimal? ExtendedValue6 { get; set; }
        string TextMessage { get; set; }
    }
}
