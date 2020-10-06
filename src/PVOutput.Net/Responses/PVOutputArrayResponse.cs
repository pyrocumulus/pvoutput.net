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

        /// <summary>
        /// Compares the response to <paramref name="other"/> for base equivalence.
        /// Api rate information is disgarded when comparing.
        /// <strong>Please note that the contents of Values are not compared.</strong>
        /// </summary>
        /// <param name="other">Other response to compare.</param>
        /// <returns>True if both responses are equivalent.</returns>
        public override bool IsEquivalentTo(PVOutputBaseResponse other)
            => base.IsEquivalentTo(other) && HasValues == ((PVOutputArrayResponse<TResponseContentType>)other).HasValues;
    }
}
