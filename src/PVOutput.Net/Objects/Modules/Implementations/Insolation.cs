using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class Insolation : IInsolation
    {
        public TimeSpan Time { get; set; }
        public int Power { get; set; }
        public int Energy { get; set; }
    }
}
