using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules
{
    public interface ISystemSearchResult
    {
        string SystemName { get; set; }
        int SystemSize { get; set; }
        int Postcode { get; set; }
        string Country { get; set; }
        string Orientation { get; set; }
        int NumberOfOutputs { get; set; }
        string LastOutput { get; set; }
        int SystemId { get; set; }
        string Panel { get; set; }
        string Inverter { get; set; }
        int? Distance { get; set; }
        double? Latitude { get; set; }
        double? Longitude { get; set; }
    }
}
