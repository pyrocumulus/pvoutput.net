using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    public interface IInsolation
    {
        DateTime Time { get; set; }
        int Power { get; set; }
        int Energy { get; set; }
    }
}
