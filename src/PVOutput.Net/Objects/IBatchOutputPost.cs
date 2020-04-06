namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single batch output used for posting multiple outputs.
    /// </summary>
    /// <inheritdoc />
    public interface IBatchOutputPost : IBaseOutputPost
    {
        /// <summary>
        /// Total energy consumed on the output date.
        /// </summary>
        int? EnergyUsed { get; set; }
    }
}
