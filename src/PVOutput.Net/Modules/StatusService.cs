using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="IStatusService"/>
    public sealed class StatusService : BaseService, IStatusService
    {
        internal StatusService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IStatus>> GetStatusForDateTimeAsync(DateTime moment, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_GetStatusForDateTime,
                [LoggingEvents.Parameter_Moment] = moment,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(moment, nameof(moment)).IsNoFutureDate();

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatus>(new GetStatusRequest { Date = moment, SystemId = systemId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IStatusHistory>> GetHistoryForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, bool ascending = false, int? systemId = null, bool extendedData = false, int? limit = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_GetHistoryForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDateTime,
                [LoggingEvents.Parameter_ToDate] = toDateTime,
                [LoggingEvents.Parameter_Ascending] = ascending,
                [LoggingEvents.Parameter_SystemId] = systemId,
                [LoggingEvents.Parameter_ExtendedData] = extendedData,
                [LoggingEvents.Parameter_Limit] = limit
            };

            Guard.Argument(toDateTime, nameof(toDateTime)).GreaterThan(fromDateTime).IsNoFutureDate();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IStatusHistory>(
                new GetStatusRequest { Date = fromDateTime.Date, From = fromDateTime, To = toDateTime, Ascending = ascending, SystemId = systemId, Extended = extendedData, Limit = limit, History = true }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IDayStatistics>> GetDayStatisticsForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_GetDayStatisticsForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDateTime,
                [LoggingEvents.Parameter_ToDate] = toDateTime,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(toDateTime, nameof(toDateTime)).GreaterThan(fromDateTime).IsNoFutureDate();

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IDayStatistics>(new GetDayStatisticsRequest { Date = fromDateTime.Date, From = fromDateTime, To = toDateTime, SystemId = systemId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> AddStatusAsync(IStatusPost status, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_AddStatus
            };

            Guard.Argument(status, nameof(status)).NotNull();

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddStatusRequest() { StatusPost = status }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchStatusAsync(IEnumerable<IBatchStatusPost> statuses, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_AddBatchStatus
            };

            Guard.Argument(statuses, nameof(statuses)).NotNull().NotEmpty();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IBatchStatusPostResult>(new AddBatchStatusRequest() { StatusPosts = statuses }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchStatusAsync(IEnumerable<IBatchStatusPost> statuses, bool isCumulative, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_AddBatchStatus,
                [LoggingEvents.Parameter_CumulativeType] = isCumulative
            };

            Guard.Argument(statuses, nameof(statuses)).NotNull().NotEmpty();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IBatchStatusPostResult>(new AddBatchStatusRequest() { StatusPosts = statuses, Cumulative = isCumulative }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchNetStatusAsync(IEnumerable<IBatchNetStatusPost> statuses, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_AddNetBatchStatus
            };

            Guard.Argument(statuses, nameof(statuses)).NotNull().NotEmpty();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IBatchStatusPostResult>(new AddBatchNetStatusRequest() { StatusPosts = statuses }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> DeleteStatusAsync(DateTime moment, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_DeleteStatus,
                [LoggingEvents.Parameter_Date] = moment
            };

            Guard.Argument(moment, nameof(moment)).IsNoFutureDate().Min(DateTime.Today.AddDays(-1));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeleteStatusRequest() { Timestamp = moment }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> DeleteAllStatusesOnDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_DeleteStatus,
                [LoggingEvents.Parameter_Date] = date
            };

            Guard.Argument(date, nameof(date)).IsNoFutureDate().Min(DateTime.Today.AddDays(-1)).NoTimeComponent();

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeleteStatusRequest() { Timestamp = date, CompleteDate = true }, loggingScope, cancellationToken);
        }
    }
}
