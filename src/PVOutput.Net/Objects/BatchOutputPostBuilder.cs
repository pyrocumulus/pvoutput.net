using Dawn;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Builder that creates batch outputs to post to PVOutput.
    /// </summary>
    public sealed class BatchOutputPostBuilder : BaseOutputPostBuilder<IBatchOutputPost, BatchOutputPostBuilder>
    {
        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public BatchOutputPostBuilder()
        {
            Reset();
        }

        /// <inheritdoc/>
        public override void Reset() => OutputPost = new BatchOutputPost();

        /// <summary>
        /// Sets the total energy consumed for the output.
        /// </summary>
        /// <param name="energyUsed">Total energy consumed.</param>
        /// <returns>The builder.</returns>
        public BatchOutputPostBuilder SetUsed(int energyUsed)
        {
            Guard.Argument(energyUsed, nameof(energyUsed)).Min(0);

            OutputPost.EnergyUsed = energyUsed;
            return this;
        }
    }
}
