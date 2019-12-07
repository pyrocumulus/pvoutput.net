using System.Collections.Generic;

namespace PVOutput.Net.Responses
{
    public sealed class PVOutputArrayResponse<TResponseContentType> : PVOutputBaseResponse
    {
        public bool HasValues { get; internal set; }

        public IEnumerable<TResponseContentType> Values { get; internal set; }
    }
}
