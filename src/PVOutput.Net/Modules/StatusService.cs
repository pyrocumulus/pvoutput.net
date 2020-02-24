using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Status service handles system status information and live output data.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getstatus">API information</see>.</para>
    /// </summary>
    public sealed class StatusService : BaseService
    {
        internal StatusService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Get the system status a a specific moment.
        /// </summary>
        /// <param name="moment">Moment to retrieve the system status for.</param>
        /// <param name="systemId">Retrieve status for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Status at the specified moment.</returns>
        public Task<PVOutputResponse<IStatus>> GetStatusForDateTimeAsync(DateTime moment, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IStatus>(new GetStatusRequest { Date = moment, SystemId = systemId }, cancellationToken);
        }

        /// <summary>
        /// Get the status history for a specific period.
        /// </summary>
        /// <param name="fromDateTime">Minimum datetime for the requested range.</param>
        /// <param name="toDateTime">Maximum datetime for the requested range.</param>
        /// <param name="ascending">Order the statuses in ascending order instead of descending.</param>
        /// <param name="systemId">Retrieve statuses for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="extendedData">Also retrieve extended data. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="limit">Limit the number of retrieved statuses.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Statusses for the specified period.</returns>
        public Task<PVOutputArrayResponse<IStatusHistory>> GetHistoryForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, bool ascending = false, int? systemId = null, bool extendedData = false, int? limit = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IStatusHistory>(
                new GetStatusRequest { Date = fromDateTime.Date, From = fromDateTime, To = toDateTime, Ascending = ascending, SystemId = systemId, Extended = extendedData, Limit = limit, History = true }, cancellationToken);
        }

        /// <summary>
        /// Gets the day statistics for specific period.
        /// </summary>
        /// <param name="fromDateTime">Minimum datetime for the requested range.</param>
        /// <param name="toDateTime">Maximum datetime for the requested range.</param>
        /// <param name="systemId">Retrieve statuses for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<PVOutputResponse<IDayStatistics>> GetDayStatisticsForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            var response = handler.ExecuteSingleItemRequestAsync<IDayStatistics>(new GetDayStatisticsRequest { Date = fromDateTime.Date, From = fromDateTime, To = toDateTime, SystemId = systemId }, cancellationToken);

            return response.ContinueWith(antecedent => AddRequestedDate(antecedent, fromDateTime.Date), cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        private static PVOutputResponse<IDayStatistics> AddRequestedDate(Task<PVOutputResponse<IDayStatistics>> response, DateTime requestedDate)
        {
            IDayStatistics statistics = response.Result.Value;
            statistics.PeakTime = requestedDate.Add(statistics.PeakTime.TimeOfDay);

            if (statistics.StandbyPowerTime != null)
            {
                statistics.StandbyPowerTime = requestedDate.Add(statistics.StandbyPowerTime.Value.TimeOfDay);
            }

            return response.Result;
        }

        /// <summary>
        /// Adds a single status to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-addstatus">API information</see>.</para>
        /// </summary>
        /// <param name="status">The status to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> AddStatusAsync(IStatusPost status, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddStatusRequest() { StatusPost = status }, cancellationToken);
        }

        /// <summary>
        /// Adds multiple statuses to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-addbatchstatus">API information</see>.</para>
        /// </summary>
        /// <param name="statuses">The statuses to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputArrayResponse<IBatchStatusPostResult>> AddBatchStatusAsync(IEnumerable<IBatchStatusPost> statuses, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IBatchStatusPostResult>(new AddBatchStatusRequest() { StatusPosts = statuses }, cancellationToken); ;
        }

        /// <summary>
        /// Deletes a status on the specified moment. 
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-deletestatus">API information</see>.</para>
        /// </summary>
        /// <param name="moment">The moment to delete the status for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> DeleteStatusAsync(DateTime moment, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeleteStatusRequest() { Timestamp = moment }, cancellationToken);
        }
    }
}
