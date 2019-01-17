using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Requests.Base
{
    internal interface IRequest
    {
        string UriTemplate { get; }

        IDictionary<string, object> GetUriPathParameters();
    }
}
