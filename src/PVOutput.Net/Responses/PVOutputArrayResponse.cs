using System.Collections.Generic;

namespace PVOutput.Net.Responses
{
    public class PVOutputArrayResponse<TResponseContentType> : PVOutputNoContentResponse
    {
        public bool HasValue { get; set; }

        public IEnumerable<TResponseContentType> Value { get; set; }
    }
}
