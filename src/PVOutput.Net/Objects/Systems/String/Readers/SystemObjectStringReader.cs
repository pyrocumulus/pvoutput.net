using PVOutput.Net.Objects.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVOutput.Net.Objects.Systems.String.Readers
{
    internal class SystemObjectStringReader : BaseObjectStringReader<ISystem>
    {
        public override ISystem CreateObjectInstance() => new Implementations.System();

        protected const char GroupSeparator = ';';

        protected override Action<ISystem, string>[] ObjectProperties
        {
            get
            {
                return new Action<ISystem, string>[]
                {
                    (target, propertyString) => target.SystemName = propertyString,
                    (target, propertyString) => target.SystemSize = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.Postcode = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.NumberOfPanels = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.PanelPower = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.PanelBrand = propertyString,
                    (target, propertyString) => target.NumberOfInverters = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.InverterPower = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.InverterBrand = propertyString,
                    (target, propertyString) => target.Orientation = propertyString,
                    (target, propertyString) => target.ArrayTilt = FormatHelper.ParseNumeric(propertyString),
                    (target, propertyString) => target.Shade = propertyString,
                    (target, propertyString) => target.InstallDate = FormatHelper.ParseDate(propertyString),
                    (target, propertyString) => target.Latitude = FormatHelper.ParseNumeric(propertyString),
                    (target, propertyString) => target.Longitude = FormatHelper.ParseNumeric(propertyString),
                    (target, propertyString) => target.StatusInterval = Convert.ToInt32(propertyString),

                    //";"

                    (target, propertyString) => target.SecondaryNumberOfPanels = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.SecondaryPanelPower = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.SecondaryOrientation = propertyString,
                    (target, propertyString) => target.SecondaryArrayTilt = FormatHelper.ParseValue<decimal>(propertyString),

                    //";"

                    (target, propertyString) => target.ExportTariff = FormatHelper.ParseValue<decimal>(propertyString),
                    (target, propertyString) => target.ImportPeakTariff = FormatHelper.ParseValue<decimal>(propertyString),
                    (target, propertyString) => target.ImportOffPeakTariff = FormatHelper.ParseValue<decimal>(propertyString),
                    (target, propertyString) => target.ImportShoulderTariff = FormatHelper.ParseValue<decimal>(propertyString),
                    (target, propertyString) => target.ImportHighShoulderTariff = FormatHelper.ParseValue<decimal>(propertyString),
                    (target, propertyString) => target.ImportDailyCharge = FormatHelper.ParseValue<decimal>(propertyString),

                    //";"

                    (target, propertyString) => target.Teams = GetTeamIdsFromValue(propertyString),

                    //";"

                    (target, propertyString) => target.Donations = FormatHelper.ParseValueDefault<int>(propertyString),

                    //";"

                    (target, propertyString) => target.ExtendedDataConfig = propertyString,

                    //";"

                    (target, propertyString) => target.MonthlyEstimations = propertyString
                };
            }
        }

        private IEnumerable<int> GetTeamIdsFromValue(string value)
        {
            return Enumerable.Empty<int>();
        }
    }
}
