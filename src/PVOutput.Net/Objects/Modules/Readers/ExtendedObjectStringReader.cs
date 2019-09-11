using System;
using System.Globalization;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class ExtendedObjectStringReader : BaseObjectStringReader<IExtended>
    {
        public override IExtended CreateObjectInstance() => new Extended();

        public ExtendedObjectStringReader()
        {
            var properties = new Action<IExtended, string>[]
            {
                (t, s) => t.Date = FormatHelper.ParseDate(s),
                (t, s) => t.ExtendedValue1 = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.ExtendedValue2 = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.ExtendedValue3 = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.ExtendedValue4 = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.ExtendedValue5 = FormatHelper.ParseValue<decimal>(s),
                (t, s) => t.ExtendedValue6 = FormatHelper.ParseValue<decimal>(s),
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
