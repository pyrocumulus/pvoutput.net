using System;
using System.Collections.Generic;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes a list of dates without any recorded data.
    /// </summary>
    public interface IMissing
    {
        /// <summary>
        /// Dates where no data has been recorded for.
        /// </summary>
        IEnumerable<DateTime> Dates { get; set; }
    }
}
