using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Builders;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Status service handles system status information and live output data.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-status-service">API information</see>.</para>
    /// </summary>
    public interface IStatusService
    {
        /// <summary>
        /// Get the system status at a specific moment.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-status-service">API information</see>.</para>
        /// </summary>
        /// <param name="moment">Moment to retrieve the system status for.</param>
        /// <param name="systemId">Retrieve status for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Status at the specified moment.</returns>
        Task<PVOutputResponse<IStatus>> GetStatusForDateTimeAsync(DateTime moment, int? systemId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the status history for a specific period.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#history-query">API information</see>.</para>
        /// </summary>
        /// <param name="fromDateTime">Minimum datetime for the requested range.</param>
        /// <param name="toDateTime">Maximum datetime for the requested range.</param>
        /// <param name="ascending">Order the statuses in ascending order instead of descending.</param>
        /// <param name="systemId">Retrieve statuses for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="extendedData">Also retrieve extended data. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="limit">Limit the number of retrieved statuses.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Statusses for the specified period.</returns>
        Task<PVOutputArrayResponse<IStatusHistory>> GetHistoryForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, bool ascending = false, int? systemId = null, bool extendedData = false, int? limit = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the day statistics for specific period.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#day-statistics">API information</see>.</para>
        /// </summary>
        /// <param name="fromDateTime">Minimum datetime for the requested range.</param>
        /// <param name="toDateTime">Maximum datetime for the requested range.</param>
        /// <param name="systemId">Retrieve statuses for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Day statistics for the specified period.</returns>
        Task<PVOutputResponse<IDayStatistics>> GetDayStatisticsForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, int? systemId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a single status to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#add-status-service">API information</see>.</para>
        /// Use the <see cref="StatusPostBuilder{TResultType}"/> to create <see cref="IStatusPost"/> objects.
        /// </summary>
        /// <param name="status">The status to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> AddStatusAsync(IStatusPost status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds multiple statuses to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#add-batch-status-service">API information</see>.</para>
        /// Use the <see cref="StatusPostBuilder{IBatchStatusPost}"/> to create <see cref="IBatchStatusPost"/> objects.
        /// </summary>
        /// <param name="statuses">The statuses to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchStatusAsync(IEnumerable<IBatchStatusPost> statuses, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds multiple statuses to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#add-batch-status-service">API information</see>.</para>
        /// Use the <see cref="StatusPostBuilder{IBatchStatusPost}"/> to create <see cref="IBatchStatusPost"/> objects.
        /// </summary>
        /// <param name="statuses">The statuses to add.</param>
        /// <param name="isCumulative">Sets whether or not the provided data is cumulative.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchStatusAsync(IEnumerable<IBatchStatusPost> statuses, bool isCumulative, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds multiple statuses to the owned system.
        /// <para>See the official <see href=https://pvoutput.org/help/api_specification.html#add-batch-status-service">API information</see>.</para>
        /// Use the <see cref="StatusPostBuilder{IBatchStatusPost}"/> to create <see cref="IBatchStatusPost"/> objects.
        /// </summary>
        /// <param name="statuses">The statuses to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchNetStatusAsync(IEnumerable<IBatchNetStatusPost> statuses, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a status on the specified moment. 
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#delete-status-service">API information</see>.</para>
        /// </summary>
        /// <param name="moment">The moment to delete the status for. This can only be today or yesterday.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> DeleteStatusAsync(DateTime moment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all statuses on the specified date. 
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#delete-status-service">API information</see>.</para>
        /// </summary>
        /// <param name="statusDate">The date to delete all statuses for. This can only be today or yesterday.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> DeleteAllStatusesOnDateAsync(DateTime statusDate, CancellationToken cancellationToken = default);
    }
}
