using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class FavouriteObjectStringReader : BaseObjectStringReader<IFavourite>
    {
        public override IFavourite CreateObjectInstance() => new Favourite();

        public FavouriteObjectStringReader()
        {
            var properties = new Action<IFavourite, string>[]
            {
                (t, s) => t.SystemId = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Postcode = FormatHelper.GetValue<int>(s),
                (t, s) => t.NumberOfPanels = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PanelPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PanelBrand = s,
                (t, s) => t.NumberOfInverters = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.InverterPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.InverterBrand = s,
                (t, s) => t.Orientation = s,
                (t, s) => t.ArrayTilt = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.Shade = s,
                (t, s) => t.InstallDate = FormatHelper.ParseOptionalDate(s),
                (t, s) => t.Location = new PVCoordinate(FormatHelper.GetValueOrDefault<decimal>(s), 0), // Latitude
                (t, s) => t.Location = new PVCoordinate(t.Location.Latitude, FormatHelper.GetValueOrDefault<decimal>(s)), // Add longitude
                (t, s) => t.StatusInterval = FormatHelper.GetValueOrDefault<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
