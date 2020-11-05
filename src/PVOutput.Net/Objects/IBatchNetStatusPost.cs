using System;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single net batch status used for posting multiple statuses.
    /// </summary>
    public interface IBatchNetStatusPost
    {
        /// <summary>
        /// Timestamp for the recorded status.
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Total energy generated up to and including the timestamp.
        /// </summary>
        int? PowerExported { get; set; }

        /// <summary>
        /// Actual power being generated at the moment of the timestamp.
        /// </summary>
        int? PowerImported { get; set; }
    }
}
