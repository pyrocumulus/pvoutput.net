using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Statistic service retrieves system statistical information.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getstatistic">API information</see>.</para>
    /// </summary>
    public sealed class StatisticsService : BaseService
    {
        internal StatisticsService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieve lifetime statistics for a system.
        /// <para><strong>Donation makes it possible to retrieve any system</strong></para>
        /// </summary>
        /// <param name="includeConsumptionAndImport">Whether or not to include consumption and import data. <strong>System owner only.</strong></param>
        /// <param name="includeCreditDebit">Whether or not to include credit and debit data. <strong>System owner only.</strong></param>
        /// <param name="systemId">Id of the system to retrieve statistical information for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Lifetime statistical information</returns>
        public Task<PVOutputResponse<IStatistic>> GetLifetimeStatisticsAsync(bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticRequest { SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeCreditDebit }, cancellationToken);
        }

        /// <summary>
        /// Retrieve lifetime statistics for a system, in a certain period.
        /// <para><strong>Donation makes it possible to retrieve any system</strong></para>
        /// </summary>
        /// <param name="fromDate">Minimum DateTime for the requested range.</param>
        /// <param name="toDate">Maximum DateTime for the requested range.</param>
        /// <param name="includeConsumptionAndImport">Whether or not to include consumption and import data. <strong>System owner only.</strong></param>
        /// <param name="includeCreditDebit">Whether or not to include credit and debit data. <strong>System owner only.</strong></param>
        /// <param name="systemId">Id of the system to retrieve statistical information for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Statistical information for the requested period.</returns>
        public Task<PVOutputResponse<IStatistic>> GetStatisticsForPeriodAsync(DateTime fromDate, DateTime toDate, bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticPeriodRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeCreditDebit }, cancellationToken);
        }
    }
}
