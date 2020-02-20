using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Aggregated live supply and consumption data.
    /// </summary>
    public interface ISupply
    {
        /// <summary>
        /// Timestamp of the aggregation.
        /// </summary>
        DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Region the aggregation is for.
        /// </summary>
        string RegionName { get; set; }

        /// <summary>
        /// Utilisation percentage of the region.
        /// </summary>
        decimal Utilisation { get; set; }

        /// <summary>
        /// Total power output for the region.
        /// </summary>
        int TotalPowerOutput { get; set; }

        /// <summary>
        /// Total power input for the region.
        /// </summary>
        int TotalPowerInput { get; set; }

        /// <summary>
        /// Average power output for the region.
        /// </summary>
        int AveragePowerOutput { get; set; }

        /// <summary>
        /// Average power input for the region.
        /// </summary>
        int AveragePowerInput { get; set; }

        /// <summary>
        /// Average net power for the region.
        /// </summary>
        int AverageNetPower { get; set; }

        /// <summary>
        /// Systems currently recording output in the region.
        /// </summary>
        int SystemsOut { get; set; }

        /// <summary>
        /// Systems currently recording input in the region.
        /// </summary>
        int SystemsIn { get; set; }
        
        /// <summary>
        /// Total size of the solar capacity in the region.
        /// </summary>
        int TotalSize { get; set; }

        /// <summary>
        /// Average system size in the region.
        /// </summary>
        int AverageSize { get; set; }
    }
}
