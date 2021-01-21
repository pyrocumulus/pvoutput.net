using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// The search result for a system search.
    /// </summary>
    public interface ISystemSearchResult
    {
        /// <summary>
        /// Name of the found system.
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        /// Size of the found system.
        /// </summary>
        int SystemSize { get; set; }

        /// <summary>
        /// Postcode of the found system.
        /// </summary>
        int Postcode { get; set; }

        /// <summary>
        /// Country the found system is in.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Orientation of the solar panels of the found system.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Total number of outputs the found system has recorded.
        /// </summary>
        int NumberOfOutputs { get; set; }

        /// <summary>
        /// Textual description of when the system last recorded an output.
        /// </summary>
        string LastOutput { get; set; }

        /// <summary>
        /// Id of the found system.
        /// </summary>
        int SystemId { get; set; }

        /// <summary>
        /// Brand of the solar panels of the found system.
        /// </summary>
        string Panel { get; set; }

        /// <summary>
        /// Brand of the primary inverter of the found system.
        /// </summary>
        string Inverter { get; set; }
        
        /// <summary>
        /// The distance from the searched location, if requested.
        /// </summary>
        int? Distance { get; set; }

        /// <summary>
        /// Location of the system.
        /// </summary>
        PVCoordinate Location { get; set; }
    }
}
