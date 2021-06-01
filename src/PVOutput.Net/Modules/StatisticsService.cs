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
    /// <inheritdoc cref="IStatisticsService"/>
    public sealed class StatisticsService : BaseService, IStatisticsService
    {
        internal StatisticsService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IStatistic>> GetLifetimeStatisticsAsync(bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatisticsService_GetLifetimeStatistics,
                [LoggingEvents.Parameter_IncludeConsumptionAndImport] = includeConsumptionAndImport,
                [LoggingEvents.Parameter_IncludeCreditDebit] = includeCreditDebit,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticRequest { SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeCreditDebit }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<IStatistic>> GetStatisticsForPeriodAsync(DateTime fromDate, DateTime toDate, bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.StatisticsService_GetStatisticsForPeriod,
                [LoggingEvents.Parameter_FromDate] = fromDate,
                [LoggingEvents.Parameter_ToDate] = toDate,
                [LoggingEvents.Parameter_IncludeConsumptionAndImport] = includeConsumptionAndImport,
                [LoggingEvents.Parameter_IncludeCreditDebit] = includeCreditDebit,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(toDate, nameof(toDate)).GreaterThan(fromDate);

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticPeriodRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeCreditDebit }, loggingScope, cancellationToken);
        }
    }
}
