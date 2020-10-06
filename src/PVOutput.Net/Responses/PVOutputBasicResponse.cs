using System;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Basic response object with no data.
    /// </summary>
    public sealed class PVOutputBasicResponse : PVOutputBaseResponse
    {
        /// <summary>
        /// The success message returned by the API after the call.
        /// </summary>
        public string SuccesMessage { get; internal set; }

        /// <summary>
        /// Compares the response to <paramref name="other"/> for base equivalence.
        /// Api rate information is disgarded when comparing.
        /// </summary>
        /// <param name="other">Other response to compare.</param>
        /// <returns>True if both responses are equivalent.</returns>
        public override bool IsEquivalentTo(PVOutputBaseResponse other) 
            => base.IsEquivalentTo(other) && SuccesMessage.Equals(((PVOutputBasicResponse)other).SuccesMessage, StringComparison.OrdinalIgnoreCase);
    }
}
