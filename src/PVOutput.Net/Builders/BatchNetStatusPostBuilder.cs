using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder that creates batch outputs to post to PVOutput.
    /// </summary>
    public sealed class BatchNetStatusPostBuilder
    {
        internal BatchNetStatusPost _statusPost { get; set; }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public BatchNetStatusPostBuilder()
        {
            _statusPost = new BatchNetStatusPost();
        }

        /// <summary>
        /// Sets the timestamp for the status.
        /// </summary>
        /// <param name="timestamp">Timestamp.</param>
        /// <returns>The builder.</returns>
        public BatchNetStatusPostBuilder SetTimeStamp(DateTime timestamp)
        {
            Guard.Argument(timestamp, nameof(timestamp)).IsNoFutureDate();

            _statusPost.Timestamp = timestamp;
            return this;
        }

        /// <summary>
        /// Sets the net power exported for the status.
        /// </summary>
        /// <param name="powerExported">Net power exported consumed.</param>
        /// <returns>The builder.</returns>
        public BatchNetStatusPostBuilder SetPowerExported(int powerExported)
        {
            Guard.Argument(powerExported, nameof(powerExported)).Min(0);

            _statusPost.PowerExported = powerExported;
            return this;
        }

        /// <summary>
        /// Sets the net power imported for the status.
        /// </summary>
        /// <param name="powerImported">Net power imported consumed.</param>
        /// <returns>The builder.</returns>
        public BatchNetStatusPostBuilder SetPowerImported(int powerImported)
        {
            Guard.Argument(powerImported, nameof(powerImported)).Min(0);

            _statusPost.PowerImported = powerImported;
            return this;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new status.
        /// </summary>
        public void Reset() => _statusPost = new BatchNetStatusPost();

        /// <summary>
        /// Uses information within the builder to return the built status.
        /// </summary>
        /// <returns>The status.</returns>
        public IBatchNetStatusPost Build()
        {
            ValidateStatus();
            return _statusPost;
        }

        /// <summary>
        /// Uses information within the builder to return the built status.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The status.</returns>
        public IBatchNetStatusPost BuildAndReset()
        {
            IBatchNetStatusPost result = Build();
            Reset();
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        private void ValidateStatus()
        {
            if (_statusPost.PowerExported == null
                && _statusPost.PowerImported == null)
            {
                throw new InvalidOperationException("Status has no power values");
            }
        }
    }
}
