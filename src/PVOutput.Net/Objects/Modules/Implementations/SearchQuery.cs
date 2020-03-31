using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class SearchQuery : ISearchQuery
    {
        public string NameStartsWith { get; set; }
        public string NameContains { get; set; }
        public int PostcodeOrPower { get; set; }
        public string Postcode { get; set; }
        public int Size { get; set; }
        public string Panel { get; set; }
        public string Inverter { get; set; }
        public int DistanceKilometers { get; set; }
        public int? DistancePostcode { get; set; }
        public string TeamName { get; set; }
        public Orientation Orientation { get; set; }
        public int Tilt { get; set; }
        public TimeSpan PreviousPeriod { get; set; }
        public DateTime SpecificDate { get; set; }
    }
}
