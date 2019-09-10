namespace PVOutput.Net.Responses
{
    public class PVOutputResponse<TResponseContentType> : PVOutputNoContentResponse
    {
        public bool HasValue { get; set; }

        public TResponseContentType Value { get; set; }
    }
}
