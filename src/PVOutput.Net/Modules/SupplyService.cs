using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="ISupplyService"/>
    internal sealed class SupplyService : BaseService, ISupplyService
    {
        internal SupplyService(PVOutputClient client) : base(client)
        {

        }

        /// <inheritdoc />
        public Task<PVOutputArrayResponse<ISupply>> GetSupplyAsync(string timeZone = null, string regionKey = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SupplyService_GetSupply,
                [LoggingEvents.Parameter_TimeZone] = timeZone,
                [LoggingEvents.Parameter_RegionKey] = regionKey
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISupply>(new SupplyRequest { TimeZone = timeZone, RegionKey = regionKey }, loggingScope, cancellationToken);
        }
    }
}
