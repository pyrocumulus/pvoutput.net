using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="IOutputService"/>
    public sealed class OutputService : BaseService, IOutputService
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
                [LoggingEvents.Parameter_GetInsolation] = getInsolation,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(outputDate, nameof(outputDate)).Max(DateTime.Today);

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
                [LoggingEvents.Parameter_GetInsolation] = getInsolation,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(toDate, nameof(toDate)).GreaterThan(fromDate).IsNoFutureDate().NoTimeComponent();
            Guard.Argument(fromDate, nameof(fromDate)).NoTimeComponent();

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

            Guard.Argument(outputDate, nameof(outputDate)).Max(DateTime.Today);

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

            Guard.Argument(toDate, nameof(toDate)).GreaterThan(fromDate).IsNoFutureDate().NoTimeComponent();
            Guard.Argument(fromDate, nameof(fromDate)).NoTimeComponent();

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

            Guard.Argument(toDate, nameof(toDate)).GreaterThan(fromDate).IsNoFutureDate().NoTimeComponent();
            Guard.Argument(fromDate, nameof(fromDate)).NoTimeComponent();

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

            Guard.Argument(output, nameof(output)).NotNull();

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

            Guard.Argument(outputs, nameof(outputs)).NotNull().NotEmpty();

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddOutputsRequest() { Outputs = outputs }, loggingScope, cancellationToken);
        }
    }
}
