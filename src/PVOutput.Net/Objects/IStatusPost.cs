using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single status used for posting single statuses.
    /// </summary>
    /// <inheritdoc />
    public interface IStatusPost : IBatchStatusPost
    {
        /// <summary>
        /// Indicates what kind of cumulative value the status has, if any.
        /// </summary>
        CumulativeStatusType Cumulative { get; set; }

        /// <summary>
        /// Indicates that the power values passed are net export/import rather than gross generation/consumption.
        /// </summary>
        bool Net { get; set; }

        /// <summary>
        /// A text message to record with the status.
        /// </summary>
        string TextMessage { get; set; }
    }
}
