using Dawn;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PVOutput.Net.DependencyInjection;
using PVOutput.Net.Modules;
using PVOutput.Net.Requests;

namespace PVOutput.Net
{
    /// <inheritdoc />
    public sealed class PVOutputClient : IPVOutputClient
    {
        internal const string PVOutputBaseUri = @"https://pvoutput.org/service/r2/";

        private IHttpClientProvider _httpClientProvider;
        internal IHttpClientProvider HttpClientProvider 
        { 
            get 
            {
                return _httpClientProvider ?? (_httpClientProvider = new HttpClientProvider());
            }
            set 
            {
                _httpClientProvider = value;
            } 
        }

        private ILogger<PVOutputClient> _logger;
        internal ILogger<PVOutputClient> Logger
        {
            get
            {
                return _logger ?? (_logger = NullLogger<PVOutputClient>.Instance);
            }
            set
            {
                _logger = value;
            }
        }


        /// <inheritdoc />
        public string ApiKey { get; set; }

        /// <inheritdoc />
        public int OwnedSystemId { get; set; }

        /// <inheritdoc />
        public bool ThrowResponseExceptions { get; set; } = true;

        /// <inheritdoc />
        public IOutputService Output { get; private set; }

        /// <inheritdoc />
        public ISystemService System { get; private set; }

        /// <inheritdoc />
        public IStatusService Status { get; private set; }

        /// <inheritdoc />
        public IStatisticsService Statistics { get; private set; }

        /// <inheritdoc />
        public IMissingService Missing { get; private set; }

        /// <inheritdoc />
        public ITeamService Team { get; private set; }

        /// <inheritdoc />
        public IExtendedService Extended { get; private set; }

        /// <inheritdoc />
        public IFavouriteService Favourite { get; private set; }

        /// <inheritdoc />
        public IInsolationService Insolation { get; private set; }

        /// <inheritdoc />
        public ISupplyService Supply { get; private set; }

        /// <inheritdoc />
        public ISearchService Search { get; private set; }

        /// <inheritdoc />
        public INotificationService Notification { get; private set; }

        /// <summary>
        /// Creates a new PVOutputClient.
        /// </summary>
        /// <param name="apiKey">ApiKey to use with authenticating.</param>
        /// <param name="ownedSystemId">Id of the currently owned system used for authenticating.</param>
        public PVOutputClient(string apiKey, int ownedSystemId)
        {            
            ApiKey = apiKey;
            OwnedSystemId = ownedSystemId;

            CreateServices();
        }

        /// <summary>
        /// Creates a new PVOutputClient, with a ILogger attached.
        /// </summary>
        /// <param name="apiKey">ApiKey to use with authenticating.</param>
        /// <param name="ownedSystemId">Id of the currently owned system used for authenticating.</param>
        /// <param name="logger">The ILogger implementation, used for logging purposes.</param>
        public PVOutputClient(string apiKey, int ownedSystemId, ILogger<PVOutputClient> logger) : this(apiKey, ownedSystemId)
        {
            Logger = logger;
        }

        /// <summary>
        /// Creates a new PVOutputClient.
        /// </summary>
        /// <param name="options">Options to use for the client, containing the ApiKey and Id of the owned system.</param>
        public PVOutputClient(PVOutputClientOptions options)
        {
            Guard.Argument(options).NotNull();

            ApiKey = options.ApiKey;
            OwnedSystemId = options.OwnedSystemId;

            CreateServices();
        }

        /// <summary>
        /// Creates a new PVOutputClient, with a ILogger attached.
        /// </summary>
        /// <param name="options">Options to use for the client, containing the ApiKey and Id of the owned system.</param>
        /// <param name="logger">The ILogger implementation, used for logging purposes.</param>
        public PVOutputClient(PVOutputClientOptions options, ILogger<PVOutputClient> logger) : this(options)
        {
            Logger = logger;
        }

        private void CreateServices()
        {
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
            Notification = new NotificationService(this);
        }
    }
}
