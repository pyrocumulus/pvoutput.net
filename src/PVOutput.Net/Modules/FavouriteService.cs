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
    public class FavouriteService : BaseService
    {
        internal FavouriteService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputArrayResponse<IFavourite>> GetFavourites(int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IFavourite>(new FavouriteRequest() { SystemId = systemId }, cancellationToken);
        }
    }
}
