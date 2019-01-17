using System;

namespace PVOutput.Net.Responses
{
    public class PVOutputNoContentResponse
    {
        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }

        public bool Equals(PVOutputNoContentResponse other)
            => other != null && IsSuccess == other.IsSuccess && Exception == other.Exception;

        public static implicit operator bool(PVOutputNoContentResponse response) => response.IsSuccess;

        public PVOutputApiRateInformation ApiRateInformation;
    }
}