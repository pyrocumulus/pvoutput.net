using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects.Modules
{
    public interface IStatusPost : IBatchStatusPost
    {
        CumulativeStatusType Cumulative { get; set; }
        bool Net { get; set; }
        string TextMessage { get; set; }
    }
}
