using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PVOutput.Net.DependencyInjection;
using PVOutput.Net.Modules;
using PVOutput.Net.Requests;
using PVOutput.Net.Requests.Handler;

namespace PVOutput.Net
{
    /// <summary>
    /// The client used to communicate with the PVOutput service.
    /// </summary>
    public sealed class PVOutputClient
    {
        internal const string PVOutputBaseUri = @"https://pvoutput.org/service/r2/";
        internal IHttpClientProvider HttpClientProvider { get; }

        internal ILogger Logger { get; }

        /// <summary>ApiKey to use with authenticating.</summary>
        public string ApiKey { get; set; }

        /// <summary>Id of the currently owned system used for authenticating.</summary>
        public int OwnedSystemId { get; set; }

        /// <summary>Indicates whether or not the client should throw exceptions when the api returns errors.</summary>
        public bool ThrowResponseExceptions { get; set; } = true;

        /// <summary>
        /// The output service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getoutput">API information</see>.</para>
        /// </summary>
        public OutputService Output { get; }

        /// <summary>
        /// The system service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsystem">API information</see>.</para>
        /// </summary>
        public SystemService System { get; }

        /// <summary>
        /// The status service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getstatus">API information</see>.</para>
        /// </summary>
        public StatusService Status { get; }

        /// <summary>
        /// The statistic service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getstatistic">API information</see>.</para>
        /// </summary>
        public StatisticsService Statistics { get; }

        /// <summary>
        /// The missing service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getmissing">API information</see>.</para>
        /// </summary>
        public MissingService Missing { get; }

        /// <summary>
        /// The team service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsupply">API information</see>.</para>
        /// </summary>
        public TeamService Team { get; }

        /// <summary>
        /// The extended service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getextended">API information</see>.</para>
        /// </summary>
        public ExtendedService Extended { get; }

        /// <summary>
        /// The favourite service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getfavourite">API information</see>.</para>
        /// </summary>
        public FavouriteService Favourite { get; }

        /// <summary>
        /// The insolation service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getinsolation">API information</see>.</para>
        /// </summary>
        public InsolationService Insolation { get; }

        /// <summary>
        /// The supply service.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsupply">API information</see>.</para>
        /// </summary>
        public SupplyService Supply { get; }

        /// <summary>
        /// The search service
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-search">API information</see>.</para>
        /// </summary>
        public SearchService Search { get; }

        /// <summary>
        /// Creates a new PVOutputClient.
        /// </summary>
        /// <param name="apiKey">ApiKey to use with authenticating.</param>
        /// <param name="ownedSystemId">Id of the currently owned system used for authenticating.</param>
        public PVOutputClient(string apiKey, int ownedSystemId) : this(apiKey, ownedSystemId, null)
        {
        }

        /// <summary>
        /// Creates a new PVOutputClient, with a ILogger attached.
        /// </summary>
        /// <param name="apiKey">ApiKey to use with authenticating.</param>
        /// <param name="ownedSystemId">Id of the currently owned system used for authenticating.</param>
        /// <param name="logger">The ILogger implementation, used for logging purposes.</param>
        public PVOutputClient(string apiKey, int ownedSystemId, ILogger<PVOutputClient> logger) : this(apiKey, ownedSystemId, new HttpClientProvider(), logger)
        {
        }

        /// <summary>
        /// Creates a new PVOutputClient, with a ILogger attached.
        /// </summary>
        /// <param name="options">Options to use for the client, containing the ApiKey and Id of the owned system.</param>
        public PVOutputClient(PVOutputClientOptions options) : this(options?.ApiKey, options.OwnedSystemId, null)
        {
        }

        /// <summary>
        /// Creates a new PVOutputClient, with a ILogger attached.
        /// </summary>
        /// <param name="options">Options to use for the client, containing the ApiKey and Id of the owned system.</param>
        /// <param name="logger">The ILogger implementation, used for logging purposes.</param>
        public PVOutputClient(PVOutputClientOptions options, ILogger<PVOutputClient> logger) : this(options?.ApiKey, options.OwnedSystemId, new HttpClientProvider(), logger)
        {
        }

        internal PVOutputClient(string apiKey, int ownedSystemId, IHttpClientProvider httpClientProvider, ILogger logger)
        {
            ApiKey = apiKey;
            OwnedSystemId = ownedSystemId;
            HttpClientProvider = httpClientProvider ?? new HttpClientProvider();
            Logger = logger ?? NullLogger.Instance;

            Output = new OutputService(this);
            System = new SystemService(this);
            Status = new StatusService(this);
            Statistics = new StatisticsService(this);
            Missing = new MissingService(this);
            Team = new TeamService(this);
            Extended = new ExtendedService(this);
            Favourite = new FavouriteService(this);
            Insolation = new InsolationService(this);
            Supply = new SupplyService(this);
            Search = new SearchService(this); 
        }
    }
}
