using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// An aggregated daily output for a system.
    /// </summary>
    public interface IAggregatedOutput
    {
        /// <summary>
        /// The aggregated date the outputs belong to.
        /// </summary>
        DateTime AggregatedDate { get; set; }

        /// <summary>
        /// Number of outputs in aggregated period.
        /// </summary>
        int Outputs { get; set; }

        /// <summary>
        /// Total energy generated in aggregated period.
        /// </summary>
        int EnergyGenerated { get; set; }

        /// <summary>
        /// System efficiency in the aggregated period.
        /// </summary>
        decimal Efficiency { get; set; }

        /// <summary>
        /// Total energy exported in aggregated period.
        /// </summary>
        int EnergyExported { get; set; }

        /// <summary>
        /// Energy consumed in aggregated period.
        /// </summary>
        int EnergyUsed { get; set; }

        /// <summary>
        /// Peak energy import in aggregated period.
        /// </summary>
        int? PeakEnergyImport { get; set; }

        /// <summary>
        /// Off peak energy import in aggregated period.
        /// </summary>
        int? OffPeakEnergyImport { get; set; }

        /// <summary>
        /// shoulder energy import in aggregated period.
        /// </summary>
        int? ShoulderEnergyImport { get; set; }

        /// <summary>
        /// High shoulder energy import in aggregated period.
        /// </summary>
        int? HighShoulderEnergyImport { get; set; }
    }
}
