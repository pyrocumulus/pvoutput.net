using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Core
{
    internal interface IObjectStringFactory<TObjectType>
    {
        IObjectStringReader<TObjectType> CreateObjectReader();
    }
}
