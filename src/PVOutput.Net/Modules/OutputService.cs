using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="IOutputService"/>
    internal sealed class OutputService : BaseService, IOutputService
    {
        internal OutputService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IOutput>> GetOutputForDateAsync(DateTime outputDate, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_GetOutputForDate,
                [LoggingEvents.Parameter_Date] = outputDate,
                [LoggingEvents.Parameter_GetInsolation] = getInsolation
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_SystemId, systemId);

            Guard.IsLessThanOrEqualTo(outputDate, DateTime.Today, nameof(outputDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IOutput>(new OutputRequest { FromDate = outputDate, ToDate = outputDate, SystemId = systemId, Insolation = getInsolation }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IOutput>> GetOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_GetOutputsForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate,
                [LoggingEvents.Parameter_GetInsolation] = getInsolation
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_SystemId, systemId);

            Guard.IsGreaterThan(toDate, fromDate, nameof(toDate));
            GuardExtensions.IsNoFutureDate(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(fromDate, nameof(fromDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, Insolation = getInsolation }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<ITeamOutput>> GetTeamOutputForDateAsync(DateTime outputDate, int teamId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_GetTeamOutputForDate,
                [LoggingEvents.Parameter_Date] = outputDate,
                [LoggingEvents.Parameter_TeamId] = teamId
            };

            Guard.IsLessThanOrEqualTo(outputDate, DateTime.Today, nameof(outputDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ITeamOutput>(new OutputRequest { FromDate = outputDate, ToDate = outputDate, TeamId = teamId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<ITeamOutput>> GetTeamOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, int teamId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_GetTeamOutputsForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate,
                [LoggingEvents.Parameter_TeamId] = teamId
            };

            Guard.IsGreaterThan(toDate, fromDate, nameof(toDate));
            GuardExtensions.IsNoFutureDate(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(fromDate, nameof(fromDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ITeamOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, TeamId = teamId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IAggregatedOutput>> GetAggregatedOutputsAsync(DateTime fromDate, DateTime toDate, AggregationPeriod period, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_GetAggregatedOutputs,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate,
                [LoggingEvents.Parameter_AggregationPeriod] = period
            };

            Guard.IsGreaterThan(toDate, fromDate, nameof(toDate));
            GuardExtensions.IsNoFutureDate(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(toDate, nameof(toDate));
            GuardExtensions.NoTimeComponent(fromDate, nameof(fromDate));

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IAggregatedOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, Aggregation = period }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> AddOutputAsync(IOutputPost output, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_AddOutput
            };

            Guard.IsNotNull(output, nameof(output));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddOutputRequest() { Output = output }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> AddOutputsAsync(IEnumerable<IOutputPost> outputs, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.OutputService_AddOutputs
            };

            Guard.IsNotNull(outputs, nameof(outputs));
            if (!outputs.Any())
            {
                throw new ArgumentException("Collection must not be empty.", nameof(outputs));
            }

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddOutputsRequest() { Outputs = outputs }, loggingScope, cancellationToken);
        }
    }
}
