﻿using System;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal sealed class BatchStatusPostResultStringReader : BaseObjectStringReader<IBatchStatusPostResult>
    {
        public override IBatchStatusPostResult CreateObjectInstance() => new Implementations.BatchStatusPostResult();

        public BatchStatusPostResultStringReader()
        {
            var properties = new Action<IBatchStatusPostResult, string>[]
            {
                (t, s) => t.Timestamp = FormatHelper.ParseDate(s),
                (t, s) => t.Timestamp = t.Timestamp.Add(FormatHelper.ParseTime(s)),
                (t, s) => t.AddedOrUpdated = FormatHelper.GetValueOrDefault<int>(s) == 1
            };

            _parsers.Add((target, reader) => ParsePropertyArray(target, reader, properties));
        }
    }
}
