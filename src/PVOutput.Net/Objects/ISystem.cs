using System;
using System.Collections.Generic;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes a system on PVOutput.
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Name of the system.
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        /// Size of the system.
        /// </summary>
        int SystemSize { get; set; }

        /// <summary>
        /// Postcode of the system.
        /// </summary>
        int Postcode { get; set; }

        /// <summary>
        /// Number of panels in the system.
        /// </summary>
        int NumberOfPanels { get; set; }

        /// <summary>
        /// Power per panel in the system.
        /// </summary>
        int PanelPower { get; set; }

        /// <summary>
        /// Brand of the solar panels in the system.
        /// </summary>
        string PanelBrand { get; set; }

        /// <summary>
        /// Number of used inverters in the system.
        /// </summary>
        int NumberOfInverters { get; set; }

        /// <summary>
        /// Total power of the primary inverter in the system.
        /// </summary>
        int InverterPower { get; set; }

        /// <summary>
        /// Brand of the primary inverter in the system.
        /// </summary>
        string InverterBrand { get; set; }

        /// <summary>
        /// Orientation of the solar panels if the system.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Tilt the solar array has.
        /// </summary>
        decimal? ArrayTilt { get; set; }

        /// <summary>
        /// The shade situation for the system.
        /// </summary>
        Shade Shade { get; set; }

        /// <summary>
        /// The date the system has been installed.
        /// </summary>
        DateTime? InstallDate { get; set; }

        /// <summary>
        /// Location of the system.
        /// </summary>
        PVCoordinate Location { get; set; }

        /// <summary>
        /// The interval the system records statuses in (5, 10 or 15 minutes).
        /// </summary>
        int StatusInterval { get; set; }

        /// <summary>
        /// Number of panels in secondary array, if present.
        /// </summary>
        int? SecondaryNumberOfPanels { get; set; }

        /// <summary>
        /// Power of panels in secondary array, if present.
        /// </summary>
        int? SecondaryPanelPower { get; set; }

        /// <summary>
        /// Orientation of secondary array, if present.
        /// </summary>
        Orientation? SecondaryOrientation { get; set; }

        /// <summary>
        /// Tilt of the secondary array, if present.
        /// </summary>
        decimal? SecondaryArrayTilt { get; set; }

        /// <summary>
        /// Export tariff of the system.
        /// </summary>
        decimal? ExportTariff { get; set; }

        /// <summary>
        /// Peak import tariff of the system.
        /// </summary>
        decimal? ImportPeakTariff { get; set; }

        /// <summary>
        /// Off peak import tariff of the system.
        /// </summary>
        decimal? ImportOffPeakTariff { get; set; }
        
        /// <summary>
        /// Shoulder import tariff of the system.
        /// </summary>
        decimal? ImportShoulderTariff { get; set; }

        /// <summary>
        /// High shoulder import tariff of the system.
        /// </summary>
        decimal? ImportHighShoulderTariff { get; set; }

        /// <summary>
        /// Daily charge for the import of the system.
        /// </summary>
        decimal? ImportDailyCharge { get; set; }

        /// <summary>
        /// List of ids the system is a member of.
        /// </summary>
        IReadOnlyList<int> Teams { get; set; }

        /// <summary>
        /// Number of donations the account owning the system has done.
        /// </summary>
        int Donations { get; set; }

        /// <summary>
        /// List of configurations of the extended data values.
        /// </summary>
        IReadOnlyList<ExtendedDataConfiguration> ExtendedDataConfig { get; set; }

        /// <summary>
        /// List of generation estimates per month.
        /// </summary>
        IReadOnlyDictionary<PVMonth, int> MonthlyGenerationEstimates { get; set; }

        /// <summary>
        /// List of consumption estimates per month.
        /// </summary>
        IReadOnlyDictionary<PVMonth, int> MonthlyConsumptionEstimates { get; set; }
    }
}
