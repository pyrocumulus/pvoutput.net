using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Core
{
    public interface IStringFactory<TObjectType>
    {
        IObjectStringReader<TObjectType> CreateObjectReader();

        IArrayStringReader<TObjectType> CreateArrayReader();
    }
}
