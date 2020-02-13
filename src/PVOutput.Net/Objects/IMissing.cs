using System;
using System.Collections.Generic;

namespace PVOutput.Net.Objects
{
    public interface IMissing
    {
        IEnumerable<DateTime> Dates { get; set; }
    }
}
