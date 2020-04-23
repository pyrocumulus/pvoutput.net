using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Defines an extended data value for a system.
    /// </summary>
    public interface IExtendedDataDefinition
    {
        /// <summary>
        /// The index of the extended data value (v7-v12).
        /// </summary>
        ExtendedDataIndex Index { get; set; }

        /// <summary>
        /// The label of the extended data value.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// The unit of the extended data value.
        /// </summary>        
        string Unit { get; set; }

        /// <summary>
        /// The hexadecimal (ffffff) displayed colour of the extended data value.
        /// </summary>
        string Colour { get; set; }

        /// <summary>
        /// The axis on which to display the extended data value.
        /// </summary>
        int? Axis { get; set; }

        /// <summary>
        /// The type of graph to display the extended data value with.
        /// </summary>
        ExtendedDataDisplayType? DisplayType { get; set; }
    }
}
