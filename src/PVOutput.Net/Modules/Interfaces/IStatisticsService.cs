using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Statistic service retrieves system statistical information.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-statistic-service">API information</see>.</para>
    /// </summary>
    public interface IStatisticsService
    {
        /// <summary>
        /// Retrieve lifetime statistics for a system.
        /// <para><strong>Donation makes it possible to retrieve any system</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-statistic-service">API information</see>.</para>
        /// </summary>
        /// <param name="includeConsumptionAndImport">Whether or not to include consumption and import data. <strong>System owner only.</strong></param>
        /// <param name="includeCreditDebit">Whether or not to include credit and debit data. <strong>System owner only.</strong></param>
        /// <param name="systemId">Id of the system to retrieve statistical information for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Lifetime statistical information</returns>
        Task<PVOutputResponse<IStatistic>> GetLifetimeStatisticsAsync(bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve lifetime statistics for a system, in a certain period.
        /// <para><strong>Donation makes it possible to retrieve any system</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-statistic-service">API information</see>.</para>
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="includeConsumptionAndImport">Whether or not to include consumption and import data. <strong>System owner only.</strong></param>
        /// <param name="includeCreditDebit">Whether or not to include credit and debit data. <strong>System owner only.</strong></param>
        /// <param name="systemId">Id of the system to retrieve statistical information for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Statistical information for the requested period.</returns>
        Task<PVOutputResponse<IStatistic>> GetStatisticsForPeriodAsync(DateTime fromDate, DateTime toDate, bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default);
    }
}
