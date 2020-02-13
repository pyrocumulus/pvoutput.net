using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PVOutput.Net.Responses
{
    public sealed class PVOutputException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public PVOutputException()
        {
        }

        public PVOutputException(string message)
            : base(message)
        {

        }

        public PVOutputException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public PVOutputException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public PVOutputException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
