using System;

namespace PVOutput.Net.Responses
{
    /// <summary>
    /// Base response all response types derive from.
    /// </summary>
    public abstract class PVOutputBaseResponse
    {
        /// <summary>
        /// Indicates whether or not the call is regarded to have succeeded.
        /// </summary>
        public bool IsSuccess { get; internal set; }
        
        /// <summary>
        /// Contains an API error if one has occurred.
        /// </summary>
        public PVOutputApiError Error { get; internal set; }

        /// <summary>
        /// Information regarding the current API rate limit.
        /// </summary>
        public PVOutputApiRateInformation ApiRateInformation { get; internal set; }

        /// <summary>
        /// Compares the response to <paramref name="other"/> for equality.
        /// </summary>
        /// <param name="other">Other response to compare.</param>
        /// <returns>True if both responses are equal.</returns>
        public bool Equals(PVOutputBaseResponse other)
            => other != null && IsSuccess == other.IsSuccess && Error == other.Error;

        /// <summary>
        /// Converts the response <see cref="IsSuccess"/> value to a boolean.
        /// </summary>
        /// <param name="response">Response to convert to boolean state.</param>
        public static implicit operator bool(PVOutputBaseResponse response) => response?.IsSuccess == true;

        /// <summary>
        /// Converts the response <see cref="IsSuccess"/> value to a boolean.
        /// </summary>
        /// <returns>True if <see cref="IsSuccess"/> is true.</returns>
        public bool ToBoolean() => IsSuccess;
    }
}
