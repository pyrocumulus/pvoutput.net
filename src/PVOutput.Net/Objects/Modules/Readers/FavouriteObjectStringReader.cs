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
                (t, s) => t.SystemId = Convert.ToInt32(s),
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = Convert.ToInt32(s),
                (t, s) => t.Postcode = FormatHelper.ParseValue<int>(s),
                (t, s) => t.NumberOfPanels = Convert.ToInt32(s),
                (t, s) => t.PanelPower = Convert.ToInt32(s),
                (t, s) => t.PanelBrand = s,
                (t, s) => t.NumberOfInverters = Convert.ToInt32(s),
                (t, s) => t.InverterPower = Convert.ToInt32(s),
                (t, s) => t.InverterBrand = s,
                (t, s) => t.Orientation = s,
                (t, s) => t.ArrayTilt = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.Shade = s,
                (t, s) => t.InstallDate = FormatHelper.ParseOptionalDate(s),
                (t, s) => t.Latitude = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.Longitude = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.StatusInterval = Convert.ToInt32(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
