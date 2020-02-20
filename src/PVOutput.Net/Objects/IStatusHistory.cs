using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes a history status.
    /// </summary>
    public interface IStatusHistory
    {
        /// <summary>
        /// Timestamp of the historic status
        /// </summary>
        DateTime StatusDate { get; set; }

        /// <summary>
        /// Total energy generated up to and including the timestamp.
        /// </summary>
        int? EnergyGeneration { get; set; }

        /// <summary>
        /// Efficiency at the moment of timestamp.
        /// </summary>
        decimal? EnergyEfficiency { get; set; }

        /// <summary>
        /// Actual power being generated at the moment of the timestamp.
        /// </summary>
        int? InstantaneousPower { get; set; }

        /// <summary>
        /// Average power at the moment of the timestamp.
        /// </summary>
        int? AveragePower { get; set; }

        /// <summary>
        /// Normalised output (kW/kW) for the status.
        /// </summary>
        decimal? NormalisedOutput { get; set; }

        /// <summary>
        /// Total energy consumed up to and including the timestamp.
        /// </summary>
        int? EnergyConsumption { get; set; }

        /// <summary>
        /// Actual power being consumed at the moment of the timestamp.
        /// </summary>
        int? PowerConsumption { get; set; }

        /// <summary>
        /// Actual temperature at the moment of the timestamp.
        /// </summary>
        decimal? Temperature { get; set; }

        /// <summary>
        /// Recorded voltage at the moment of the timestamp.
        /// </summary>
        decimal? Volts { get; set; }

        /// <summary>
        /// First extended value.
        /// </summary>
        decimal? ExtendedValue1 { get; set; }

        /// <summary>
        /// Second extended value.
        /// </summary>
        decimal? ExtendedValue2 { get; set; }

        /// <summary>
        /// Third extended value.
        /// </summary>
        decimal? ExtendedValue3 { get; set; }

        /// <summary>
        /// Fourth extended value.
        /// </summary>
        decimal? ExtendedValue4 { get; set; }

        /// <summary>
        /// Fifth extended value.
        /// </summary>
        decimal? ExtendedValue5 { get; set; }

        /// <summary>
        /// Sixth extended value.
        /// </summary>
        decimal? ExtendedValue6 { get; set; }
    }
}
