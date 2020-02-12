using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class SystemSearchResult : ISystemSearchResult
    {
        public string SystemName { get; set; }
        public int SystemSize { get; set; }
        public int Postcode { get; set; }
        public string Country { get; set; }
        public string Orientation { get; set; }
        public int NumberOfOutputs { get; set; }
        public string LastOutput { get; set; }
        public int SystemId { get; set; }
        public string Panel { get; set; }
        public string Inverter { get; set; }
        public int? Distance { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
