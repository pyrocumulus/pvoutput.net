using PVOutput.Net.Modules;
using PVOutput.Net.Requests.Handler;

namespace PVOutput.Net
{
    public class PVOutputClient
    {
        internal readonly string PVOutputBaseUri = @"https://pvoutput.org/service/r2/";
        internal IHttpClientProvider HttpClientProvider { get; }

        public string Apikey { get; set; }
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

        public PVOutputClient(IHttpClientProvider httpClientProvider = default)
        {
            HttpClientProvider = httpClientProvider ?? new HttpClientProvider(this);

            Output = new OutputService(this);
            System = new SystemService(this);
            Status = new StatusService(this);
            Statistics = new StatisticsService(this);
            Missing = new MissingService(this);
            Team = new TeamService(this);
            Extended = new ExtendedService(this);
            Favourite = new FavouriteService(this);
            Insolation = new InsolationService(this);
        }

        public PVOutputClient(string apiKey, int ownedSystemId, IHttpClientProvider httpClientProvider = default)
            : this(httpClientProvider)
        {
            Apikey = apiKey;
            OwnedSystemId = ownedSystemId;
        }
    }
}
