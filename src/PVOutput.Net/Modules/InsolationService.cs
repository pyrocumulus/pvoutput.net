using System;
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
    /// <inheritdoc cref="IInsolationService"/>
    public sealed class InsolationService : BaseService, IInsolationService
    {
        internal InsolationService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForOwnSystemAsync(DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.InsolationService_GetInsolationForOwnSystem,
                [LoggingEvents.Parameter_Date] = date
            };

            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { Date = date }, loggingScope, cancellationToken);
            return response;
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForSystemAsync(int systemId, DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.InsolationService_GetInsolationForSystem,
                [LoggingEvents.Parameter_SystemId] = systemId,
                [LoggingEvents.Parameter_Date] = date
            };

            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { SystemId = systemId, Date = date }, loggingScope, cancellationToken);
            return response;
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForLocationAsync(PVCoordinate coordinate, DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.InsolationService_GetInsolationForLocation,
                [LoggingEvents.Parameter_Coordinate] = coordinate,
                [LoggingEvents.Parameter_Date] = date
            };

            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { Coordinate = coordinate, Date = date }, loggingScope, cancellationToken);
            return response;
        }
    }
}
