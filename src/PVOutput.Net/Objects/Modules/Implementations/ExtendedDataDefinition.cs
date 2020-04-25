using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal class ExtendedDataDefinition : IExtendedDataDefinition
    {
        public ExtendedDataIndex Index { get; set; }
        public string Label { get; set; }
        public string Unit { get; set; }
        public string Colour { get; set; }
        public int? Axis { get; set; }
        public ExtendedDataDisplayType? DisplayType { get; set; }
    }
}
