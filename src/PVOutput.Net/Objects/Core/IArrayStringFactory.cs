namespace PVOutput.Net.Objects.Core
{
    internal interface IArrayStringFactory<TObjectType> : IObjectStringFactory<TObjectType>
    {
        IArrayStringReader<TObjectType> CreateArrayReader();
    }
}
