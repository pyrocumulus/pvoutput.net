using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <inheritdoc cref="IStatusService"/>
    internal sealed class StatusService : BaseService, IStatusService
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
                [LoggingEvents.Parameter_Moment] = moment
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_SystemId, systemId);

            GuardExtensions.IsNoFutureDate(moment, nameof(moment));

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
                [LoggingEvents.Parameter_ExtendedData] = extendedData
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_SystemId, systemId);
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_Limit, limit);

            Guard.IsGreaterThan(toDateTime, fromDateTime, nameof(toDateTime));
            GuardExtensions.IsNoFutureDate(toDateTime, nameof(toDateTime));

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
                [LoggingEvents.Parameter_ToDate] = toDateTime
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_SystemId, systemId);

            Guard.IsGreaterThan(toDateTime, fromDateTime, nameof(toDateTime));
            GuardExtensions.IsNoFutureDate(toDateTime, nameof(toDateTime));

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

            Guard.IsNotNull(status, nameof(status));

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

            GuardExtensions.NotEmpty(statuses, nameof(statuses));

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

            GuardExtensions.NotEmpty(statuses, nameof(statuses));

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

            GuardExtensions.NotEmpty(statuses, nameof(statuses));

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

            GuardExtensions.IsNoFutureDate(moment, nameof(moment));
            Guard.IsGreaterThanOrEqualTo(moment, DateTime.Today.AddDays(-1), nameof(moment));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeleteStatusRequest() { Timestamp = moment }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> DeleteAllStatusesOnDateAsync(DateTime statusDate, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatusService_DeleteStatus,
                [LoggingEvents.Parameter_Date] = statusDate
            };

            GuardExtensions.IsNoFutureDate(statusDate, nameof(statusDate));
            Guard.IsGreaterThanOrEqualTo(statusDate, DateTime.Today.AddDays(-1), nameof(statusDate));
            GuardExtensions.NoTimeComponent(statusDate, nameof(statusDate));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeleteStatusRequest() { Timestamp = statusDate, CompleteDate = true }, loggingScope, cancellationToken);
        }
    }
}
