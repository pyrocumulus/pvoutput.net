using System;
using System.Collections.Generic;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class System : ISystem
    {
        // Primary
        public string SystemName { get; set; }
        public int SystemSize { get; set; }
        public int Postcode { get; set; }
        public int NumberOfPanels { get; set; }
        public int PanelPower { get; set; }
        public string PanelBrand { get; set; }
        public int NumberOfInverters { get; set; }
        public int InverterPower { get; set; }
        public string InverterBrand { get; set; }
        public Orientation Orientation { get; set; }
        public decimal ArrayTilt { get; set; }
        public string Shade { get; set; }
        public DateTime InstallDate { get; set; }
        public PVCoordinate Location { get; set; }
        public int StatusInterval { get; set; }

        // Secondary
        public int? SecondaryNumberOfPanels { get; set; }
        public int? SecondaryPanelPower { get; set; }
        public string SecondaryOrientation { get; set; }
        public decimal? SecondaryArrayTilt { get; set; }

        // Tariffs
        public decimal? ExportTariff { get; set; }
        public decimal? ImportPeakTariff { get; set; }
        public decimal? ImportOffPeakTariff { get; set; }
        public decimal? ImportShoulderTariff { get; set; }
        public decimal? ImportHighShoulderTariff { get; set; }
        public decimal? ImportDailyCharge { get; set; }

        // Teams
        public IReadOnlyList<int> Teams { get; set; }

        // Donations
        public int Donations { get; set; }

        // Extended data config
        public IReadOnlyList<ExtendedDataConfiguration> ExtendedDataConfig { get; set; }

        // Estimates - Only owner system
        public IReadOnlyDictionary<PVMonth, int> MonthlyGenerationEstimates { get; set; }
        public IReadOnlyDictionary<PVMonth, int> MonthlyConsumptionEstimates { get; set; }
    }
}
