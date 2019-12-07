using System;

namespace PVOutput.Net.Responses
{
    public sealed class PVOutputApiRateInformation
    {
        public int CurrentLimit { get; internal set; }
        public int LimitRemaining { get; internal set; }
        public DateTime LimitResetAt { get; internal set; }
    }
}
