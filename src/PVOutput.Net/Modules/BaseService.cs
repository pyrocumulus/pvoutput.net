namespace PVOutput.Net.Modules
{
    public abstract class BaseService
    {
        public PVOutputClient Client { get; }

        public BaseService(PVOutputClient client)
        {
            Client = client;
        }
    }
}
