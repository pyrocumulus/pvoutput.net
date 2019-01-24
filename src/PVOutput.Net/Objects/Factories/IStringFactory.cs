using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Objects.Factories
{
    public interface IStringFactory<TObjectType>
    {
        IObjectStringReader<TObjectType> CreateObjectReader();

        IArrayStringReader<TObjectType> CreateArrayReader();
    }
}
