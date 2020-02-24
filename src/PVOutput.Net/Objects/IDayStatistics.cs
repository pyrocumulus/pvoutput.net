using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes the statistics for a day.
    /// </summary>
    public interface IDayStatistics
    {

        /// <summary>
        /// Total energy generated for the day.
        /// </summary>
        int EnergyGeneration { get; set; }

        /// <summary>
        /// Actual power being generated at the moment of the request.
        /// </summary>
        int PowerGeneration { get; set; }

        /// <summary>
        /// Peak power recorded on the day.
        /// </summary>
        int PeakPower { get; set; }

        /// <summary>
        /// Time the peak power was recorded on the day.
        /// </summary>
        DateTime PeakTime { get; set; }

        /// <summary>
        /// Total energy consumed for the day.
        /// </summary>
        int? EnergyConsumption { get; set; }

        /// <summary>
        /// Actual power being consumed at the moment of the request.
        /// </summary>
        int? PowerConsumption { get; set; }

        /// <summary>
        /// Standby power consumption on the day.
        /// </summary>
        int? StandbyPower { get; set; }

        /// <summary>
        /// Time the standby power was measured.
        /// </summary>
        DateTime? StandbyPowerTime { get; set; }

        /// <summary>
        /// Minimum temperature recorded on the day.
        /// </summary>
        decimal? MinimumTemperature { get; set; }

        /// <summary>
        /// Maximum temperature recorded on the day.
        /// </summary>
        decimal? MaximumTemperature { get; set; }

        /// <summary>
        /// Average temperature recorded on the day.
        /// </summary>
        decimal? AverageTemperature { get; set; }
    }
}
