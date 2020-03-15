using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get System service retrieves system information.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsystem">API information</see>.</para>
    /// </summary>
    public sealed class SystemService : BaseService
    {
        internal SystemService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieves information for the owned system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the owned system.</returns>
        public Task<PVOutputResponse<ISystem>> GetOwnSystemAsync(CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest(), cancellationToken);
        }

        /// <summary>
        /// Retrieves information for a system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="systemId">Id of the system to request information for</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the requested system.</returns>
        public Task<PVOutputResponse<ISystem>> GetOtherSystemAsync(int systemId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest { SystemId = systemId, MonthlyEstimates = false }, cancellationToken);
        }
    }
}
