using System;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Missing service retrieves a list of output dates missing from a system.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getmissing">API information</see>.</para>
    /// </summary>
    public sealed class MissingService : BaseService
    {
        internal MissingService(PVOutputClient client) : base(client)
        {

        }

        /// <summary>
        /// Retrieves a list of output dates with no data
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of missing dates</returns>
        public Task<PVOutputResponse<IMissing>> GetMissingDaysInPeriodAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            Guard.Argument(toDate, nameof(toDate)).GreaterThan(fromDate);

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IMissing>(new MissingRequest { FromDate = fromDate, ToDate = toDate }, cancellationToken);
        }
    }
}
