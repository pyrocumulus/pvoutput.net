namespace PVOutput.Net.Objects
{
    public interface IOutputPost : IBatchOutputPost
    {
        int? Consumption { get; set; }
    }
}
