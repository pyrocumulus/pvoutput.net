using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Contains the possible shade of a PV system.
    /// </summary>
    public enum Shade
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("No")]
        None = 0,

        /// <summary>
        /// Low
        /// </summary>
        [Description("Low")]
        Low = 1,

        /// <summary>
        /// Medium
        /// </summary>
        [Description("Medium")]
        Medium = 2,

        /// <summary>
        /// High
        /// </summary>
        [Description("High")]
        High = 3
    }
}
