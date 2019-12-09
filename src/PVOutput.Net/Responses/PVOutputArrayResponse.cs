using System.Collections.Generic;

namespace PVOutput.Net.Responses
{
    public sealed class PVOutputArrayResponse<TResponseContentType> : PVOutputBaseResponse
    {
        public bool HasValues => Values != null;

        public IEnumerable<TResponseContentType> Values { get; internal set; }
    }
}
