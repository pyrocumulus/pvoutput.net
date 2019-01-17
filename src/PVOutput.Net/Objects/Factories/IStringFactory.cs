using PVOutput.Net.Objects.String;

namespace PVOutput.Net.Objects.Factories
{
    public interface IStringFactory<TObjectType>
    {
        IObjectStringReader<TObjectType> CreateObjectReader();

        IArrayStringReader<TObjectType> CreateArrayReader();
    }
}