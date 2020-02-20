using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Describes the result of a batch status post operation.
    /// </summary>
    public interface IBatchStatusPostResult
    {
        /// <summary>
        /// Timestamp a status has been processed for.
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Whether or not the status has modified data.
        /// </summary>
        bool AddedOrUpdated { get; set; }
    }
}
