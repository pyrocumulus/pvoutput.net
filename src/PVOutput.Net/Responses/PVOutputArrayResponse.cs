using System.Collections.Generic;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Array response for <typeparamref name="TResponseContentType"/> objects
    /// </summary>
    /// <typeparam name="TResponseContentType">A type of returned content.</typeparam>
    public sealed class PVOutputArrayResponse<TResponseContentType> : PVOutputBaseResponse
    {
        /// <summary>
        /// Indicates whether or not the array contains any values.
        /// </summary>
        public bool HasValues => Values != null;

        /// <summary>
        /// List of the actual <typeparamref name="TResponseContentType"/> values in the response.
        /// </summary>
        public IEnumerable<TResponseContentType> Values { get; internal set; }
    }
}
