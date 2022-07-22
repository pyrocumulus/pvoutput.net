using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Contains the possible orientations of a PV system.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// North
        /// </summary>
        [Description("N")]
        North = 0,

        /// <summary>
        /// NorthEast
        /// </summary>
        [Description("NE")]
        NorthEast = 1,

        /// <summary>
        /// East
        /// </summary>
        [Description("E")]
        East = 2,

        /// <summary>
        /// SouthEast
        /// </summary>
        [Description("SE")]
        SouthEast = 3,

        /// <summary>
        /// South
        /// </summary>
        [Description("S")]
        South = 4,

        /// <summary>
        /// SouthWest
        /// </summary>
        [Description("SW")]
        SouthWest = 5,

        /// <summary>
        /// West
        /// </summary>
        [Description("W")]
        West = 6,

        /// <summary>
        /// NorthWest
        /// </summary>
        [Description("NW")]
        NorthWest = 7,

        /// <summary>
        /// EastWest
        /// </summary>
        [Description("EW")]
        EastWest = 8
    }
}
