using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes a location using a Latitude and Longitude.
    /// </summary>
    public readonly struct PVCoordinate : IEquatable<PVCoordinate>
    {
        /// <summary>
        /// Latitudinal part of the coordinate. 
        /// </summary>
        public decimal Latitude { get; }

        /// <summary>
        /// Longitudinal part of the coordinate.
        /// </summary>
        public decimal Longitude { get; }

        /// <summary>
        /// Creates a new coordinate.
        /// </summary>
        /// <param name="latitude">Latitude for the location.</param>
        /// <param name="longitude">Longitude for the location.</param>
        public PVCoordinate(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Returns the coordinate as a string.
        /// </summary>
        /// <returns>Coordinate string.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:N6},{1:N6}", Latitude, Longitude);
        }

        /// <summary>
        /// Indicates if the current object is equal to another object of the same type
        /// </summary>
        /// <param name="obj">Other object to compare to</param>
        /// <returns>True if both objects are equal, false otherwise</returns>
        public override bool Equals(object obj) => obj is PVCoordinate coordinate && Equals(coordinate);

        /// <summary>
        /// Indicates if the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">Other object to compare to</param>
        /// <returns>True if both objects are equal, false otherwise</returns>
        public bool Equals(PVCoordinate other) => Latitude == other.Latitude && Longitude == other.Longitude;

        /// <summary>
        /// Returns the hash code for this element
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            var hashCode = -1416534245;
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            return hashCode;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool operator ==(PVCoordinate left, PVCoordinate right) => left.Equals(right);
        public static bool operator !=(PVCoordinate left, PVCoordinate right) => !(left == right);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
