using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class SystemSearchResult : ISystemSearchResult
    {
        public required string SystemName { get; set; }
        public int SystemSize { get; set; }
        public int Postcode { get; set; }
        public required string Country { get; set; }
        public Orientation Orientation { get; set; }
        public int NumberOfOutputs { get; set; }
        public required string LastOutput { get; set; }
        public int SystemId { get; set; }
        public required string Panel { get; set; }
        public required string Inverter { get; set; }
        public int? Distance { get; set; }
        public PVCoordinate Location { get; set; }
    }
}
