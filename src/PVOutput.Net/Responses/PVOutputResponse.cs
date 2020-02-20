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
    }
}
