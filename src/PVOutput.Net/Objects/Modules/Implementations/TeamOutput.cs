using System;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class TeamOutput : ITeamOutput
    {
        public DateTime OutputDate { get; set; }
        public int Outputs { get; set; }
        public decimal Efficiency { get; set; }
        public int TotalGeneration { get; set; }
        public int AverageGeneration { get; set; }
        public int TotalExported { get; set; }
        public int TotalConsumption { get; set; }
        public int AverageConsumption { get; set; }
        public int TotalImported { get; set; }
    }
}
