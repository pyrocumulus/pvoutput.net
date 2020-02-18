using System;
using System.Collections.Generic;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Element describing a configured extended data value
    /// </summary>
    public struct ExtendedDataConfiguration : IEquatable<ExtendedDataConfiguration>
    {
        /// <summary>The label that the extended value has</summary>
        public string Label { get; }

        /// <summary>The unit the extended value has</summary>
        public string Unit { get; }

        internal ExtendedDataConfiguration(string label, string unit)
        {
            Label = label;
            Unit = unit;
        }

        /// <summary>
        /// Indicates if the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">Other object to compare to</param>
        /// <returns>True if both objects are equal, false otherwise</returns>
        public override bool Equals(object other) => other is ExtendedDataConfiguration element && Equals(element);

        /// <summary>
        /// Indicates if the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">Other object to compare to</param>
        /// <returns>True if both objects are equal, false otherwise</returns>
        public bool Equals(ExtendedDataConfiguration other) => Label == other.Label && Unit == other.Unit;

        /// <summary>
        /// Returns the hash code for this element
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            var hashCode = -1553767860;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Label);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Unit);
            return hashCode;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool operator ==(ExtendedDataConfiguration left, ExtendedDataConfiguration right) => left.Equals(right);
        public static bool operator !=(ExtendedDataConfiguration left, ExtendedDataConfiguration right) => !(left == right);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
