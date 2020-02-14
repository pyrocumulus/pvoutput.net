namespace PVOutput.Net.Modules
{
    /// <summary>
    /// Base that all the PVOutput services derive from
    /// </summary>
    public abstract class BaseService
    {
        internal PVOutputClient Client { get; }

        internal BaseService(PVOutputClient client)
        {
            Client = client;
        }
    }
}
