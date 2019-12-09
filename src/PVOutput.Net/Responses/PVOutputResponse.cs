namespace PVOutput.Net.Responses
{
    public sealed class PVOutputResponse<TResponseContentType> : PVOutputBaseResponse
    {
        public bool HasValue => Value != null;

        public TResponseContentType Value { get; internal set; }
    }
}
