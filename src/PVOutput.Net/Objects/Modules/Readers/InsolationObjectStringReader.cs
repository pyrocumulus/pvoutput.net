using System;
using System.Globalization;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class InsolationObjectStringReader : BaseObjectStringReader<IInsolation>
    {
        public override IInsolation CreateObjectInstance() => new Insolation();

        public InsolationObjectStringReader()
        {
            var properties = new Action<IInsolation, string>[]
            {
                (t, s) => t.Time = FormatHelper.ParseTime(s),
                (t, s) => t.Power = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Energy = FormatHelper.GetValueOrDefault<int>(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
