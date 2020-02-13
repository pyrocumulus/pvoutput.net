using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects
{
    public interface IExtended
    {
        DateTime ExtendedDate { get; set; }
        decimal? ExtendedValue1 { get; set; }
        decimal? ExtendedValue2 { get; set; }
        decimal? ExtendedValue3 { get; set; }
        decimal? ExtendedValue4 { get; set; }
        decimal? ExtendedValue5 { get; set; }
        decimal? ExtendedValue6 { get; set; }
    }
}
