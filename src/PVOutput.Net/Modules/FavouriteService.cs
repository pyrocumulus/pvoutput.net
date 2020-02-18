using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Favourite service retrieves the systems in your favourites or another user's favourites.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getfavourite">API information</see>.</para>
    /// </summary>
    public sealed class FavouriteService : BaseService
    {
        internal FavouriteService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieves the favourites for the owned system, or another.
        /// </summary>
        /// <param name="systemId">Optional SystemId to request the favourites for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of favourites.</returns>
        public Task<PVOutputArrayResponse<IFavourite>> GetFavouritesAsync(int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IFavourite>(new FavouriteRequest() { SystemId = systemId }, cancellationToken);
        }
    }
}
