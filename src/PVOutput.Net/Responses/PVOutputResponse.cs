namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Response for a <typeparamref name="TResponseContentType"/> object.
    /// </summary>
    /// <typeparam name="TResponseContentType">A type of returned content.</typeparam>
    public sealed class PVOutputResponse<TResponseContentType> : PVOutputBaseResponse
    {
        /// <summary>
        /// Indicates whether or not the response has a value.
        /// </summary>
        public bool HasValue => Value != null;

        /// <summary>
        /// The actual <typeparamref name="TResponseContentType"/> value in the response.
        /// </summary>
        public TResponseContentType Value { get; internal set; }

        /// <summary>
        /// Compares the response to <paramref name="other"/> for base equivalence.
        /// Api rate information is disgarded when comparing.
        /// <strong>Please note that the Value is not compared.</strong>
        /// </summary>
        /// <param name="other">Other response to compare.</param>
        /// <returns>True if both responses are equivalent.</returns>
        public override bool IsEquivalentTo(PVOutputBaseResponse other)
            => base.IsEquivalentTo(other) && HasValue == ((PVOutputResponse<TResponseContentType>)other).HasValue;
    }
}
