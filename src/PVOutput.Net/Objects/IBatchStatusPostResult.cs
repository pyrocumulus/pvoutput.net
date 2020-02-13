using System;

namespace PVOutput.Net.Objects
{
    public interface IBatchStatusPostResult
    {
        DateTime Timestamp { get; set; }
        bool AddedOrUpdated { get; set; }
    }
}
