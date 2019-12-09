namespace PVOutput.Net.Objects.Modules
{
    public interface IOutputPost : IBatchOutputPost
    {
        int? Consumption { get; set; }
    }
}
