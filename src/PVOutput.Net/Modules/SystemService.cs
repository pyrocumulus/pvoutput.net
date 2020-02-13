using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    public sealed class SystemService : BaseService
    {
        internal SystemService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputResponse<ISystem>> GetOwnSystem(CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest(), cancellationToken);
        }

        public Task<PVOutputResponse<ISystem>> GetOtherSystem(int systemId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest { SystemId = systemId, MonthlyEstimates = false }, cancellationToken);
        }
    }
}
