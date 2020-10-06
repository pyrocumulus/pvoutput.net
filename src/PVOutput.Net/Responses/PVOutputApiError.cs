using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Error in communicating with the API.
    /// </summary>
    public sealed class PVOutputApiError
    {
        /// <summary>
        /// Statuscode returned by the API call.
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// Message the API returned.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Compares the error to <paramref name="other"/> for base equivalence.
        /// </summary>
        /// <param name="other">Other error to compare.</param>
        /// <returns>True if both errors are equivalent.</returns>
        public bool IsEquivalentTo(PVOutputApiError other) 
            => other != null && StatusCode == other.StatusCode && Message.Equals(other.Message, StringComparison.OrdinalIgnoreCase);
    }
}
