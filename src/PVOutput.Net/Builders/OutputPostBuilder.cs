using System.Collections.Generic;
using System.Resources;
using System.Text;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder that creates outputs to post to PVOutput.
    /// </summary>
    public sealed class OutputPostBuilder : BaseOutputPostBuilder<IOutputPost, OutputPostBuilder>
    {
        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public OutputPostBuilder()
        {
            Reset();
        }

        /// <inheritdoc/>
        public override void Reset() => OutputPost = new OutputPost();

        /// <summary>
        /// Sets high shoulder energy import for the output.
        /// </summary>
        /// <param name="highShoulderImport">High shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetHighShoulderEnergyImport(int highShoulderImport)
        {
            Guard.Argument(highShoulderImport, nameof(highShoulderImport)).Min(0);

            (OutputPost as OutputPost).HighShoulderEnergyImport = highShoulderImport;
            return this;
        }

        /// <summary>
        /// Sets the total energy consumption for the output.
        /// </summary>
        /// <param name="consumption">Energy consumption</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetConsumption(int consumption)
        {
            Guard.Argument(consumption, nameof(consumption)).Min(0);

            (OutputPost as OutputPost).Consumption = consumption;
            return this;
        }
    }
}
