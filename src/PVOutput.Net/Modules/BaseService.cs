namespace PVOutput.Net.Modules
{
    public abstract class BaseService
    {
        internal PVOutputClient Client { get; }

        internal BaseService(PVOutputClient client)
        {
            Client = client;
        }
    }
}
