using PVOutput.Net.Modules;
using PVOutput.Net.Requests;
using PVOutput.Net.Requests.Handler;

namespace PVOutput.Net
{
    public sealed class PVOutputClient
    {
        internal const string PVOutputBaseUri = @"https://pvoutput.org/service/r2/";
        internal IHttpClientProvider HttpClientProvider { get; }

        public string ApiKey { get; set; }
        public int OwnedSystemId { get; set; }
        public bool ThrowResponseExceptions { get; set; } = true;

        public OutputService Output { get; }
        public SystemService System { get; }
        public StatusService Status { get; }
        public StatisticsService Statistics { get; }
        public MissingService Missing { get; }
        public TeamService Team { get; }
        public ExtendedService Extended { get; }
        public FavouriteService Favourite { get; }
        public InsolationService Insolation { get; }
        public SupplyService Supply { get; }
        public SearchService Search { get; }

        public PVOutputClient(string apiKey, int ownedSystemId) : this(new HttpClientProvider())
        {
            ApiKey = apiKey;
            OwnedSystemId = ownedSystemId;
        }

        internal PVOutputClient(IHttpClientProvider httpClientProvider)
        {
            HttpClientProvider = httpClientProvider ?? new HttpClientProvider();

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

        internal PVOutputClient(string apiKey, int ownedSystemId, IHttpClientProvider httpClientProvider) : this(httpClientProvider)
        {
            ApiKey = apiKey;
            OwnedSystemId = ownedSystemId;
        }
    }
}
