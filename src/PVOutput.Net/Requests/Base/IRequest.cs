using System.Collections.Generic;

namespace PVOutput.Net.Requests.Base
{
    internal interface IRequest
    {
        string UriTemplate { get; }

        IDictionary<string, object> GetUriPathParameters();
    }
}
