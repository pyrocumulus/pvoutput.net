using System;

namespace PVOutput.Net.Objects.Modules
{
    public interface IBatchStatusPostResult
    {
        DateTime Timestamp { get; set; }
        bool AddedOrUpdated { get; set; }
    }
}
