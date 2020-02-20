using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes a team.
    /// </summary>
    public interface ITeam
    {
        /// <summary>
        /// Name of the team.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Total size of the systems in the team.
        /// </summary>
        int TeamSize { get; set; }

        /// <summary>
        /// Average system size of the systems in the team.
        /// </summary>
        int AverageSize { get; set; }

        /// <summary>
        /// Number of systems in the team.
        /// </summary>
        int NumberOfSystems { get; set; }

        /// <summary>
        /// Total energy generated in the team.
        /// </summary>
        long EnergyGenerated { get; set; }

        /// <summary>
        /// Total number of outputs the team has recorded.
        /// </summary>
        int Outputs { get; set; }

        /// <summary>
        /// Average energy the team has generated.
        /// </summary>
        int EnergyAverage { get; set; }

        /// <summary>
        /// The type (category) of the team.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// A description of the team.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Date the team was created.
        /// </summary>
        DateTime CreationDate { get; set; }
    }
}
