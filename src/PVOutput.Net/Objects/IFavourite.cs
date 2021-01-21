using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A favourite system.
    /// </summary>
    public interface IFavourite
    {
        /// <summary>
        /// Id for the system.
        /// </summary>
        int SystemId { get; set; }

        /// <summary>
        /// Name of the system.
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        /// Size of the system.
        /// </summary>
        int SystemSize { get; set; }

        /// <summary>
        /// Postcode of the system.
        /// </summary>
        int? Postcode { get; set; }

        /// <summary>
        /// The number of panels the system has.
        /// </summary>
        int NumberOfPanels { get; set; }

        /// <summary>
        /// Power per panel in the system.
        /// </summary>
        int PanelPower { get; set; }

        /// <summary>
        /// Brand of the panels used in the system.
        /// </summary>
        string PanelBrand { get; set; }

        /// <summary>
        /// Number of used inverters in the system.
        /// </summary>
        int NumberOfInverters { get; set; }

        /// <summary>
        /// Power of the primary inverter in the system.
        /// </summary>
        int InverterPower { get; set; }

        /// <summary>
        /// Brand of the primary inverter in the system.
        /// </summary>
        string InverterBrand { get; set; }

        /// <summary>
        /// Primary orientation of most panels.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Tilt the solar array has.
        /// </summary>
        decimal? ArrayTilt { get; set; }

        /// <summary>
        /// The shade situation for the system.
        /// </summary>
        Shade Shade { get; set; }

        /// <summary>
        /// The date the system has been installed.
        /// </summary>
        DateTime? InstallDate { get; set; }

        /// <summary>
        /// Location of the system.
        /// </summary>
        PVCoordinate Location { get; set; }

        /// <summary>
        /// The interval the system records statuses in (5, 10 or 15 minutes).
        /// </summary>
        int StatusInterval { get; set; }
    }
}
