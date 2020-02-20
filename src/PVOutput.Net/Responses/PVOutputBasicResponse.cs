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
    }
}
