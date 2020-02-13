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
                (t, s) => t.ExtendedDate = FormatHelper.ParseDate(s),
                (t, s) => t.ExtendedValue1 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue2 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue3 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue4 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue5 = FormatHelper.GetValue<decimal>(s),
                (t, s) => t.ExtendedValue6 = FormatHelper.GetValue<decimal>(s),
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
