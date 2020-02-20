using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes an output for a team.
    /// </summary>
    public interface ITeamOutput
    {
        /// <summary>
        /// The date of the team output.
        /// </summary>
        DateTime OutputDate { get; set; }

        /// <summary>
        /// Number of system outputs in this teams output.
        /// </summary>
        int Outputs { get; set; }

        /// <summary>
        /// Efficiency of the teams output.
        /// </summary>
        decimal Efficiency { get; set; }

        /// <summary>
        /// Total generation of the team on the date.
        /// </summary>
        int TotalGeneration { get; set; }

        /// <summary>
        /// Average generation of the team on the date.
        /// </summary>
        int AverageGeneration { get; set; }

        /// <summary>
        /// Total energy exported of the team.
        /// </summary>
        int TotalExported { get; set; }

        /// <summary>
        /// Total energy consumed of the team.
        /// </summary>
        int TotalConsumption { get; set; }

        /// <summary>
        /// Average energy consumed in the team.
        /// </summary>
        int AverageConsumption { get; set; }

        /// <summary>
        /// Total energy imported by the team.
        /// </summary>
        int TotalImported { get; set; }
    }
}
