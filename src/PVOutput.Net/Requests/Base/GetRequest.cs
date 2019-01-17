using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PVOutput.Net.Requests.Base
{
    public abstract class GetRequest<TResponseContentType> : IRequest
    {
        public abstract HttpMethod Method { get; }

        public abstract string UriTemplate { get; }

        public abstract IDictionary<string, object> GetUriPathParameters();
    }
}
