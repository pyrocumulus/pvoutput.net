using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    public class MissingService : BaseService
    {
        public MissingService(PVOutputClient client) : base(client)
        {

        }

        public Task<PVOutputResponse<IMissing>> GetMissingDaysInPeriod(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IMissing>(new MissingRequest { FromDate = fromDate, ToDate = toDate }, cancellationToken);
        }
    }
}
