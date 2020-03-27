using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Insolation service retrieves 5-minute insolation data (power and energy) under ideal weather conditions.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getinsolation">API information</see>.</para>
    /// <para><strong>Note: this is a donation only service.</strong></para>
    /// </summary>
    public sealed class InsolationService : BaseService
    {
        internal InsolationService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Get insolation data for own system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="date">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the owned system.</returns>
        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForOwnSystemAsync(DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.InsolationService_GetInsolationForOwnSystem,
                [LoggingEvents.Parameter_Date] = date
            };

            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { Date = date }, loggingScope, cancellationToken);

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
            }
            return response;
        }

        /// <summary>
        /// Get insolation data for a system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="systemId">Id of the system to get insolation for.</param>
        /// <param name="date">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the requested system.</returns>
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

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
            }
            return response;
        }

        /// <summary>
        /// Get insolation data for a location.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="coordinate">GPS coordinate, to request insolation for.</param>
        /// <param name="date">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the requested location.</returns>
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

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
            }
            return response;
        }

        private static PVOutputArrayResponse<IInsolation> AddRequestedDate(Task<PVOutputArrayResponse<IInsolation>> response, DateTime requestedDate)
        {
            foreach (var insolation in response.Result.Values)
            {
                insolation.Time = requestedDate.Add(insolation.Time.TimeOfDay);
            }
            return response.Result;
        }
    }
}
