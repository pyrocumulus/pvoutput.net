using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    public class ExtendedService : BaseService
    {
        public ExtendedService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputResponse<IExtended>> GetRecentExtendedDataAsync(CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<IExtended>(new ExtendedRequest(), cancellationToken);
        }

        public Task<PVOutputArrayResponse<IExtended>> GetExtendedDataForPeriodAsync(DateTime fromDate, DateTime toDate, int? limit = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<IExtended>(new ExtendedRequest() { FromDate = fromDate, ToDate = toDate, Limit = limit }, cancellationToken);
        }
    }
}
