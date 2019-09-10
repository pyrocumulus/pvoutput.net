using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Modules
{
    public interface ITeam
    {
        string Name { get; set; }
        int TeamSize { get; set; }
        int AverageSize { get; set; }
        int NumberOfSystems { get; set; }
        long EnergyGenerated { get; set; }
        int Outputs { get; set; }
        int EnergyAverage { get; set; }
        string Type { get; set; }
        string Description { get; set; }
        DateTime CreationDate { get; set; }
    }
}
