using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="IExtendedService"/>
    internal sealed class ExtendedService : BaseService, IExtendedService
    {
        internal ExtendedService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IExtended>> GetRecentExtendedDataAsync(CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.ExtendedService_GetRecentExtendedData
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IExtended>(new ExtendedRequest(), loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IExtended>> GetExtendedDataForPeriodAsync(DateTime fromDate, DateTime toDate, int? limit = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.ExtendedService_GetExtendedDataForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate,
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_Limit, limit);

            Guard.IsGreaterThan(toDate, fromDate, nameof(toDate));
            if (limit.HasValue)
                Guard.IsLessThan(limit.Value, 50, nameof(limit));

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IExtended>(new ExtendedRequest() { FromDate = fromDate, ToDate = toDate, Limit = limit }, loggingScope, cancellationToken);
        }
    }
}
