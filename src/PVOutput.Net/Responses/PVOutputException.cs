using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// A <see cref="PVOutputException"/> is thrown when the API call returns an error.
    /// </summary>
    public sealed class PVOutputException : Exception
    {
        /// <summary>
        /// Statuscode returned by the API call.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Creates a new <see cref="PVOutputException"/> with a default statuscode and empty message.
        /// </summary>
        public PVOutputException() : base("")
        {
            StatusCode = default;
        }

        /// <summary>
        /// Creates a new <see cref="PVOutputException"/> with a default statuscode and specified message.
        /// </summary>
        /// <param name="message">Message for the exception.</param>
        public PVOutputException(string message)
            : base(message)
        {
            StatusCode = default;
        }

        /// <summary>
        /// Creates a new <see cref="PVOutputException"/> with a specific HttpStatusCode and empty message.
        /// </summary>
        /// <param name="statusCode">Statuscode for the exception.</param>
        public PVOutputException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a new <see cref="PVOutputException"/> with a specific HttpStatusCode and message.
        /// </summary>
        /// <param name="statusCode">Statuscode for the exception.</param>
        /// <param name="message">Message for the exception.</param>
        public PVOutputException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Wraps an exception in a <see cref="PVOutputException"/> with a default statuscode and specified message.
        /// </summary>
        /// <param name="message">Message for the exception.</param>
        /// <param name="inner">Inner exception that occurred.</param>
        public PVOutputException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = default;
        }
    }
}
