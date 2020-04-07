using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes the daily output for a system.
    /// </summary>
    public interface IOutput
    {
        /// <summary>
        /// Date the output was recorded at.
        /// </summary>
        DateTime OutputDate { get; set; }

        /// <summary>
        /// Total energy generated on the output date.
        /// </summary>
        int EnergyGenerated { get; set; }

        /// <summary>
        /// Efficiency for the date.
        /// </summary>
        decimal Efficiency { get; set; }

        /// <summary>
        /// Total energy exported on the output date.
        /// </summary>
        int EnergyExported { get; set; }

        /// <summary>
        /// Total energy consumed on the date.
        /// </summary>
        int EnergyUsed { get; set; }

        /// <summary>
        /// Peak power recorded on the output date.
        /// </summary>
        int? PeakPower { get; set; }

        /// <summary>
        /// Time the peak power was recorded on the output date.
        /// </summary>
        TimeSpan? PeakTime { get; set; }

        /// <summary>
        /// Solar conditions on the output date.
        /// </summary>
        WeatherCondition Condition { get; set; }

        /// <summary>
        /// Minimum temperature recorded on the output date.
        /// </summary>
        int? MinimumTemperature { get; set; }

        /// <summary>
        /// Maximum temperature recorded on the output date.
        /// </summary>
        int? MaximumTemperature { get; set; }

        /// <summary>
        /// Peak energy import on the output date.
        /// </summary>
        int? PeakEnergyImport { get; set; }

        /// <summary>
        /// Off peak energy import on the output date.
        /// </summary>
        int? OffPeakEnergyImport { get; set; }

        /// <summary>
        /// Shoulder energy import on the output date.
        /// </summary>
        int? ShoulderEnergyImport { get; set; }

        /// <summary>
        /// High shoulder energy import on the output date.
        /// </summary>
        int? HighShoulderEnergyImport { get; set; }

        /// <summary>
        /// Isolation (theoretic maximum output) value for the output date
        /// </summary>
        int? Insolation { get; set; }
    }
}
