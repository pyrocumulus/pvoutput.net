using System;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Rate information for the API.
    /// </summary>
    public sealed class PVOutputApiRateInformation
    {
        /// <summary>
        /// Hourly limit currently being enforced on account.
        /// </summary>
        public int CurrentLimit { get; internal set; }

        /// <summary>
        /// Remaining API calls for the hour.
        /// </summary>
        public int LimitRemaining { get; internal set; }

        /// <summary>
        /// UNIX timestamp when the limit will be reset.
        /// </summary>
        public DateTime LimitResetAt { get; internal set; }
    }
}
