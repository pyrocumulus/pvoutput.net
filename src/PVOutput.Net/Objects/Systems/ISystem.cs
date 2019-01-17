using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Systems
{
    public interface ISystem
    {
        string SystemName { get; set; }
        int SystemSize { get; set; }
        int Postcode { get; set; }
        int NumberOfPanels { get; set; }
        int PanelPower { get; set; }
        string PanelBrand { get; set; }
        int NumberOfInverters { get; set; }
        int InverterPower { get; set; }
        string InverterBrand { get; set; }
        string Orientation { get; set; }
        decimal ArrayTilt { get; set; }
        string Shade { get; set; }
        DateTime InstallDate { get; set; }
        decimal Latitude { get; set; }
        decimal Longitude { get; set; }
        int StatusInterval { get; set; }

        int? SecondaryNumberOfPanels { get; set; }
        int? SecondaryPanelPower { get; set; }
        string SecondaryOrientation { get; set; }
        decimal? SecondaryArrayTilt { get; set; }

        // Tariffs
        decimal? ExportTariff { get; set; }
        decimal? ImportPeakTariff { get; set; }
        decimal? ImportOffPeakTariff { get; set; }
        decimal? ImportShoulderTariff { get; set; }
        decimal? ImportHighShoulderTariff { get; set; }
        decimal? ImportDailyCharge { get; set; }

        // Teams
        IEnumerable<int> Teams { get; set; }

        // Donations
        int Donations { get; set; }

        // Extended data config
        string ExtendedDataConfig { get; set; }

        // Estimates - Only owner system
        string MonthlyEstimations { get; set; }
    }
}
