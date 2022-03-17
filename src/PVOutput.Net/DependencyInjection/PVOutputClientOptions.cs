namespace PVOutput.Net.DependencyInjection
{
    /// <summary>
    /// Options used to create a PVOutputClient through Microsoft's Dependency Injection.
    /// </summary>
    public sealed class PVOutputClientOptions
    {
        /// <summary>ApiKey to use with authenticating.</summary>
        public string ApiKey { get; set; }

        /// <summary>Id of the currently owned system used for authenticating.</summary>
        public int OwnedSystemId { get; set; }
    }
}
