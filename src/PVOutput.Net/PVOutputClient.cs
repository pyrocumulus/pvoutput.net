using PVOutput.Net.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public PVOutputClient(IHttpClientProvider httpClientProvider = default)
        {
            HttpClientProvider = httpClientProvider ?? new HttpClientProvider(this);

            Output = new OutputService(this);
            System = new SystemService(this);
			Status = new StatusService(this);
        }

        public PVOutputClient(string apiKey, int ownedSystemId, IHttpClientProvider httpClientProvider = default) 
            : this(httpClientProvider)
        {
            Apikey = apiKey;
            OwnedSystemId = ownedSystemId;
        }
    }
}
