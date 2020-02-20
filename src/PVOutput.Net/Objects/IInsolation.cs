using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single insolation value.
    /// </summary>
    public interface IInsolation
    {
        /// <summary>
        /// Time of day of the insolation value.
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// Expected power at the time of day.
        /// </summary>
        int Power { get; set; }

        /// <summary>
        /// Expected energy at the time of day.
        /// </summary>
        int Energy { get; set; }
    }
}
