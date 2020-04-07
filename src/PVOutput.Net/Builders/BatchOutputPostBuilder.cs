using System;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Builders
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
        public BatchOutputPostBuilder SetEnergyUsed(int energyUsed)
        {
            Guard.Argument(energyUsed, nameof(energyUsed)).Min(0);

            OutputPost.EnergyUsed = energyUsed;
            return this;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        protected internal override void ValidateStatus()
        {
            base.ValidateStatus();

            if (OutputPost.EnergyGenerated == null && OutputPost.EnergyUsed == null)
            {
                throw new InvalidOperationException($"Either {nameof(OutputPost.EnergyGenerated)} or {nameof(OutputPost.EnergyUsed)} has to have a value.");
            }
        }
    }
}
