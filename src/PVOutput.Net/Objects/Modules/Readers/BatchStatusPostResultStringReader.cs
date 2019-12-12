using System;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class BatchStatusPostResultStringReader : BaseObjectStringReader<IBatchStatusPostResult>
    {
        public override IBatchStatusPostResult CreateObjectInstance() => new Implementations.BatchStatusPostResult();

        public BatchStatusPostResultStringReader()
        {
            var properties = new Action<IBatchStatusPostResult, string>[]
            {
                (t, s) => t.Timestamp = FormatHelper.ParseDate(s),
                (t, s) => t.Timestamp = s.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? t.Timestamp : t.Timestamp.Add(FormatHelper.ParseTime(s).TimeOfDay),
                (t, s) => t.AddedOrUpdated = FormatHelper.ParseValueWithDefault<int>(s) == 1
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
