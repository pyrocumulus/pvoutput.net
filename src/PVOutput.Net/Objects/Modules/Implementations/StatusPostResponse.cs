using System;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class BatchStatusPostResult : IBatchStatusPostResult
    {
        public DateTime Timestamp { get; set; }
        public bool AddedOrUpdated { get; set; }
    }
}
