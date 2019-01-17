using System;

namespace PVOutput.Net.Responses
{
    public class PVOutputApiRateInformation
    {
        public int CurrentLimit { get; set; }
        public int LimitRemaining { get; set; }
        public DateTime LimitResetAt { get; set; }
    }
}