using System;
using System.Collections.Generic;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class Missing : IMissing
    {
        public IEnumerable<DateTime> Dates { get; set; }
    }
}
