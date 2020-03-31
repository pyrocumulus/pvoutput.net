using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A search query used to search for systems.
    /// </summary>
    public interface ISearchQuery
    {
        /// <summary>
        /// System name should start with.
        /// </summary>
        string NameStartsWith { get; set; }

        /// <summary>
        /// System name should contain.
        /// </summary>
        string NameContains { get; set; }

        /// <summary>
        /// Postcode or total size should begin with numeric value.
        /// </summary>
        int PostcodeOrPower { get; set; }

        /// <summary>
        /// Postcode should begin with value.
        /// </summary>
        string Postcode { get; set; }

        /// <summary>
        /// Total size should begin with numeric value.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Panel name should contain the value.
        /// </summary>
        string Panel { get; set; }

        /// <summary>
        /// Inverter name should contain the value.
        /// </summary>
        string Inverter { get; set; }

        /// <summary>
        /// Find systems within these kilometers from current system (or <see cref="DistancePostcode"/>).
        /// </summary>
        int DistanceKilometers { get; set; }

        /// <summary>
        /// Find systems within <see cref="DistanceKilometers"/> from this postcode.
        /// </summary>
        int? DistancePostcode { get; set; }

        /// <summary>
        /// Team name should contain value.
        /// </summary>
        string TeamName { get; set; }

        /// <summary>
        /// Panel orientation should be equal to this.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Tilt should be within 2.5 degrees of this value.
        /// </summary>
        int Tilt { get; set; }

        /// <summary>
        /// Only return systems with outputs in this period.
        /// </summary>
        TimeSpan PreviousPeriod { get; set; }

        /// <summary>
        /// Only return systems with outputs on this date.
        /// </summary>
        DateTime SpecificDate { get; }
    }
}
