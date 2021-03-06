﻿using System.Collections.Generic;
using System.Net.Http;

namespace PVOutput.Net.Requests.Base
{
    internal abstract class GetRequest<TResponseContentType> : IRequest
    {
        public abstract HttpMethod Method { get; }

        public abstract string UriTemplate { get; }

        public abstract IDictionary<string, object> GetUriPathParameters();
    }
}
