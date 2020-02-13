using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Core
{
    internal interface IStringFactory<TObjectType>
    {
        IObjectStringReader<TObjectType> CreateObjectReader();

        IArrayStringReader<TObjectType> CreateArrayReader();
    }
}
