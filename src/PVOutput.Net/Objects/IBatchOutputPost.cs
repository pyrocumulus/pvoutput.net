using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single batch output used for posting multiple outputs.
    /// </summary>
    public interface IBatchOutputPost
    {
        /// <summary>
        /// The date the output is for.
        /// </summary>
        DateTime OutputDate { get; set; }

        /// <summary>
        /// Total energy generated on the output date.
        /// </summary>
        int? EnergyGenerated { get; set; }

        /// <summary>
        /// Total energy exported on the output date.
        /// </summary>
        int? EnergyExported { get; set; }

        /// <summary>
        /// Peak power recorded on the output date.
        /// </summary>
        int? PeakPower { get; set; }

        /// <summary>
        /// Time the peak power was recorded on the output date.
        /// </summary>
        DateTime? PeakTime { get; set; }

        /// <summary>
        /// Solar conditions on the output date.
        /// </summary>
        WeatherCondition Condition { get; set; }

        /// <summary>
        /// Minimum temperature recorded on the output date.
        /// </summary>
        decimal? MinimumTemperature { get; set; }

        /// <summary>
        /// Maximum temperature recorded on the output date.
        /// </summary>
        decimal? MaximumTemperature { get; set; }

        /// <summary>
        /// Comments for the recorded output.
        /// </summary>
        string Comments { get; set; }

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
    }
}
