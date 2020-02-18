using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Extended service retrieves system daily extended data.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getextended">API information</see>.</para>
    /// <para><strong>Note: this is a donation only service.</strong></para>
    /// </summary>
    public sealed class ExtendedService : BaseService
    {
        internal ExtendedService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieve most recently created extended data.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Most recent extended data.</returns>
        public Task<PVOutputResponse<IExtended>> GetRecentExtendedDataAsync(CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IExtended>(new ExtendedRequest(), cancellationToken);
        }

        /// <summary>
        /// Retrieve extended data for a certain period.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="fromDate">Minimum DateTime for the requested range.</param>
        /// <param name="toDate">Maximum DateTime for the requested range.</param>
        /// <param name="limit">Optional limit of number of items. <c>50 is the maximum.</c></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of extended data objects.</returns>
        public Task<PVOutputArrayResponse<IExtended>> GetExtendedDataForPeriodAsync(DateTime fromDate, DateTime toDate, int? limit = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IExtended>(new ExtendedRequest() { FromDate = fromDate, ToDate = toDate, Limit = limit }, cancellationToken);
        }
    }
}
