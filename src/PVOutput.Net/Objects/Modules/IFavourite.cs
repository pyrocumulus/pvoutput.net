using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules
{
    public interface IFavourite
    {
        int SystemId { get; set; }
        string SystemName { get; set;}
        int SystemSize { get; set; }
        int? Postcode { get; set; }
        int NumberOfPanels { get; set; }
        int PanelPower { get; set; }
        string PanelBrand { get; set; }
        int NumberOfInverters { get; set; }
        int InverterPower { get; set; }
        string InverterBrand { get; set; }
        string Orientation { get; set; }
        decimal? ArrayTilt { get; set; }
        string Shade { get; set; }
        DateTime? InstallDate { get; set; }
        decimal? Latitude { get; set; }
        decimal? Longitude { get; set; }
        int StatusInterval { get; set; }
    }
}
