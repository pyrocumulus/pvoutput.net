using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single batch status used for posting multiple statuses.
    /// </summary>
    public interface IBatchStatusPost
    {
        /// <summary>
        /// Timestamp for the recorded status.
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Total energy generated up to and including the timestamp.
        /// </summary>
        int? EnergyGeneration { get; set; }

        /// <summary>
        /// Actual power being generated at the moment of the timestamp.
        /// </summary>
        int? PowerGeneration { get; set; }

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
        decimal? Voltage { get; set; }

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
