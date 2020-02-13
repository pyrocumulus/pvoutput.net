using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PVOutput.Net.Responses
{
    public sealed class PVOutputApiError
    {
        public HttpStatusCode StatusCode { get; internal set; }
        public string Message { get; internal set; }
    }
}
