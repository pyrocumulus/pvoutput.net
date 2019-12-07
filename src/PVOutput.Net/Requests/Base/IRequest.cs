using System.Collections.Generic;
using System.Net.Http;

namespace PVOutput.Net.Requests.Base
{
    internal interface IRequest
    {
        string UriTemplate { get; }

        HttpMethod Method { get; }

        IDictionary<string, object> GetUriPathParameters();
    }
}
