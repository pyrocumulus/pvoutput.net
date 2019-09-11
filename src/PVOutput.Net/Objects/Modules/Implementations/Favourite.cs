using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class Favourite : IFavourite
    {
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public int SystemSize { get; set; }
        public int? Postcode { get; set; }
        public int NumberOfPanels { get; set; }
        public int PanelPower { get; set; }
        public string PanelBrand { get; set; }
        public int NumberOfInverters { get; set; }
        public int InverterPower { get; set; }
        public string InverterBrand { get; set; }
        public string Orientation { get; set; }
        public decimal? ArrayTilt { get; set; }
        public string Shade { get; set; }
        public DateTime? InstallDate { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int StatusInterval { get; set; }
    }
}
