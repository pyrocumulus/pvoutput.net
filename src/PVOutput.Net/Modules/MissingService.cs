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
    /// <inheritdoc cref="IMissingService"/>
    internal sealed class MissingService : BaseService, IMissingService
    {
        internal MissingService(PVOutputClient client) : base(client)
        {

        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IMissing>> GetMissingDaysInPeriodAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.MissingService_GetMissingDaysInPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate
            };

            Guard.IsGreaterThan(toDate, fromDate, nameof(toDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IMissing>(new MissingRequest { FromDate = fromDate, ToDate = toDate }, loggingScope, cancellationToken);
        }
    }
}
