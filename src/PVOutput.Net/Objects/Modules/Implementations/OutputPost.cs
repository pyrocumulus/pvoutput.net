using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class OutputPost : BaseOutputPost, IOutputPost
    {
        public int? HighShoulderEnergyImport { get; set; }
        public int? Consumption { get; set; }
    }
}
