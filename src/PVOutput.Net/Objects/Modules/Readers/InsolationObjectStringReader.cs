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
                (t, s) => t.Power = Convert.ToInt32(s),
                (t, s) => t.Energy = Convert.ToInt32(s)
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
