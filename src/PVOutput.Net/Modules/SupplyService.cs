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
    /// The Get Supply service displays aggregated live generation and consumption data.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsupply">API information</see>.</para>
    /// </summary>
    public sealed class SupplyService : BaseService
    {
        internal SupplyService(PVOutputClient client) : base(client)
        {

        }

        /// <summary>
        /// Retrieves live generation and consumption data
        /// </summary>
        /// <param name="timeZone">A timezone identifier in the 'Europe/London' format. Will default to GMT+0 if empty.</param>
        /// <param name="regionKey">Region key in the '1:queensland' format. <strong>Donation only option.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of supply information</returns>
        public Task<PVOutputArrayResponse<ISupply>> GetSupplyAsync(string timeZone = null, string regionKey = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<ISupply>(
                new SupplyRequest { TimeZone = timeZone, RegionKey = regionKey }, cancellationToken);
        }
    }
}
