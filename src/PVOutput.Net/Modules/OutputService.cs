using PVOutput.Net.Objects.Outputs;
using PVOutput.Net.Requests;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Outputs;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Modules
{
    public class OutputService : BaseService
    {
        public OutputService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputResponse<IOutput>> GetOutputForDateAsync(DateTime day, bool getInsolation, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<IOutput>(new OutputRequest { FromDate = day, ToDate = day, SystemId = systemId, Insolation = getInsolation }, cancellationToken);
        }

        public Task<PVOutputArrayResponse<IOutput>> GetOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, bool getInsolation, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<IOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, Insolation = getInsolation }, cancellationToken);
        }

        public Task<PVOutputResponse<ITeamOutput>> GetTeamOutputForDateAsync(DateTime day, int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<ITeamOutput>(new OutputRequest { FromDate = day, ToDate = day, TeamId = teamId }, cancellationToken);
        }

        public Task<PVOutputArrayResponse<ITeamOutput>> GetTeamOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<ITeamOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, TeamId = teamId }, cancellationToken);
        }

        public Task<PVOutputArrayResponse<IAggregatedOutput>> GetAggregatedOutputsAsync(DateTime fromDate, DateTime toDate, AggregationPeriod period, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<IAggregatedOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, AggregationPeriod = period }, cancellationToken);
        }
    }
}
