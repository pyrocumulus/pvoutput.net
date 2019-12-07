namespace PVOutput.Net.Responses
{
    public sealed class PVOutputResponse<TResponseContentType> : PVOutputBaseResponse
    {
        public bool HasValue { get; internal set; }

        public TResponseContentType Value { get; internal set; }
    }
}
