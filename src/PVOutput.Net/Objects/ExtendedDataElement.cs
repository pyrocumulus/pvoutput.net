using System;
using System.Collections.Generic;

namespace PVOutput.Net.Objects
{
    public struct ExtendedDataElement : IEquatable<ExtendedDataElement>
    {
        public string Label { get; }
        public string Unit { get; }

        public ExtendedDataElement(string label, string unit)
        {
            Label = label;
            Unit = unit;
        }

        public override bool Equals(object obj) => obj is ExtendedDataElement element && Equals(element);
        public bool Equals(ExtendedDataElement other) => Label == other.Label && Unit == other.Unit;

        public override int GetHashCode()
        {
            var hashCode = -1553767860;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Label);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Unit);
            return hashCode;
        }

        public static bool operator ==(ExtendedDataElement left, ExtendedDataElement right) => left.Equals(right);
        public static bool operator !=(ExtendedDataElement left, ExtendedDataElement right) => !(left == right);

        //public override bool Equals(object obj) => base.Equals(obj);

        //public override int GetHashCode()
        //{
        //    int hash = 17;
        //    return 17 * (23 + Label.GetHashCode()) * (23 + Unit.GetHashCode());
        //}
    }
}
