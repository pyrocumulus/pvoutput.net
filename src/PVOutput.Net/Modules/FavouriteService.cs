using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="IFavouriteService"/>
    public sealed class FavouriteService : BaseService, IFavouriteService
    {
        internal FavouriteService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IFavourite>> GetFavouritesAsync(int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.FavouriteService_GetFavourites,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IFavourite>(new FavouriteRequest() { SystemId = systemId }, loggingScope, cancellationToken);
        }
    }
}
