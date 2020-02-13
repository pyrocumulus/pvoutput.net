using System;

namespace PVOutput.Net.Responses
{
    public abstract class PVOutputBaseResponse
    {
        public bool IsSuccess { get; internal set; }
        
        public PVOutputApiError Error { get; internal set; }

        public PVOutputApiRateInformation ApiRateInformation { get; internal set; }

        public bool Equals(PVOutputNoContentResponse other)
            => other != null && IsSuccess == other.IsSuccess && Error == other.Error;

        public static implicit operator bool(PVOutputBaseResponse response) => response.IsSuccess;
    }
}
