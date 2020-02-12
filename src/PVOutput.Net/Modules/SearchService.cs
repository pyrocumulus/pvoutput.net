using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    public class SearchService : BaseService
    {
        public SearchService(PVOutputClient client) : base(client)
        {
            
        }

        public Task<PVOutputArrayResponse<ISystemSearchResult>> Search(string searchQuery, double? latitude = null, double? longitude = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery, Latitude = latitude, Longitude = longitude }, cancellationToken);
        }
    }
}
