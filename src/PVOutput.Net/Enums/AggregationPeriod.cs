using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Contains the periods by which data can be aggregated
    /// </summary>
    public enum AggregationPeriod { 
        /// <summary>
        /// Aggregate by month
        /// </summary>
        Month,

        /// <summary>
        /// Aggregate by year
        /// </summary>
        Year
    };
}
