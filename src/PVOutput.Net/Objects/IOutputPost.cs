namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single output used for posting outputs.
    /// </summary>
    /// <inheritdoc />
    public interface IOutputPost : IBaseOutputPost
    {
        /// <summary>
        /// High shoulder energy import on the output date.
        /// </summary>
        int? HighShoulderEnergyImport { get; set; }

        /// <summary>
        /// The total consumption on the output date.
        /// </summary>
        int? Consumption { get; set; }
    }
}
