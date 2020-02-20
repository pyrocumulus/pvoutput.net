namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single output used for posting outputs.
    /// </summary>
    /// <inheritdoc />
    public interface IOutputPost : IBatchOutputPost
    {
        /// <summary>
        /// The total consumption on the output date.
        /// </summary>
        int? Consumption { get; set; }
    }
}
