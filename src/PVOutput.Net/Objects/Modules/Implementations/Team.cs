using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules.Implementations
{
    internal sealed class Team : ITeam
    {
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public int AverageSize { get; set; }
        public int NumberOfSystems { get; set; }
        public long EnergyGenerated { get; set; }
        public int Outputs { get; set; }
        public int EnergyAverage { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
