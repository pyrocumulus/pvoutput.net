using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    public interface IStatusPost : IBatchStatusPost
    {
        CumulativeStatusType Cumulative { get; set; }
        bool Net { get; set; }
        string TextMessage { get; set; }
    }
}
