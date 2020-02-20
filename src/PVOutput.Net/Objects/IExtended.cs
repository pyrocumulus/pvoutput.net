using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Object describing extended values for a date.
    /// </summary>
    public interface IExtended
    {
        /// <summary>
        /// Date the extended values are for.
        /// </summary>
        DateTime ExtendedDate { get; set; }

        /// <summary>
        /// First extended value.
        /// </summary>
        decimal? ExtendedValue1 { get; set; }

        /// <summary>
        /// Second extended value.
        /// </summary>
        decimal? ExtendedValue2 { get; set; }

        /// <summary>
        /// Third extended value.
        /// </summary>
        decimal? ExtendedValue3 { get; set; }

        /// <summary>
        /// Fourth extended value.
        /// </summary>
        decimal? ExtendedValue4 { get; set; }

        /// <summary>
        /// Fifth extended value.
        /// </summary>
        decimal? ExtendedValue5 { get; set; }

        /// <summary>
        /// Sixth extended value.
        /// </summary>
        decimal? ExtendedValue6 { get; set; }
    }
}
