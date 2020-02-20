using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Output service handles daily output information for individual systems.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getoutput">API information</see>.</para>
    /// </summary>
    public sealed class OutputService : BaseService
    {
        internal OutputService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieve daily output for a date.
        /// </summary>
        /// <param name="date">Date to retrieve the output for.</param>
        /// <param name="getInsolation">Also retrieve insolation information. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="systemId">Retrieve output for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Output for the requested date.</returns>
        public Task<PVOutputResponse<IOutput>> GetOutputForDateAsync(DateTime date, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<IOutput>(new OutputRequest { FromDate = date, ToDate = date, SystemId = systemId, Insolation = getInsolation }, cancellationToken);
        }

        /// <summary>
        /// Retrieve daily outputs for a period.
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="getInsolation">Also retrieve insolation information. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="systemId">Retrieve output for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Outputs for the requested period.</returns>
        public Task<PVOutputArrayResponse<IOutput>> GetOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<IOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, Insolation = getInsolation }, cancellationToken);
        }

        /// <summary>
        /// Retrieve daily output for a team.
        /// </summary>
        /// <param name="date">Date to retrieve the output for.</param>
        /// <param name="teamId">Team to retrieve the output for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team output for the requested date.</returns>
        public Task<PVOutputResponse<ITeamOutput>> GetTeamOutputForDateAsync(DateTime date, int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteSingleItemRequestAsync<ITeamOutput>(new OutputRequest { FromDate = date, ToDate = date, TeamId = teamId }, cancellationToken);
        }

        /// <summary>
        /// Retrieve daily team outputs for a period.
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="teamId">Team to retrieve the output for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team outputs Outputs for the requested period.</returns>
        public Task<PVOutputArrayResponse<ITeamOutput>> GetTeamOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<ITeamOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, TeamId = teamId }, cancellationToken);
        }

        /// <summary>
        /// Retrieve aggregated outputs for a period.
        /// </summary>
        /// <param name="fromDate">Minimum DateTime for the requested range.</param>
        /// <param name="toDate">Maximum DateTime for the requested range.</param>
        /// <param name="period">Period to aggregate the outputs in.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Aggregated outputs for the requested period.</returns>
        public Task<PVOutputArrayResponse<IAggregatedOutput>> GetAggregatedOutputsAsync(DateTime fromDate, DateTime toDate, AggregationPeriod period, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<IAggregatedOutput>(new OutputRequest { FromDate = fromDate, ToDate = toDate, Aggregation = period }, cancellationToken);
        }

        /// <summary>
        /// Adds a single daily output to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-addoutput">API information</see>.</para>
        /// </summary>
        /// <param name="output">The output to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> AddOutputAsync(IOutputPost output, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddOutputRequest() { Output = output }, cancellationToken);
        }

        /// <summary>
        /// Adds a list of outputs to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-addbatchoutput">API information</see>.</para>
        /// </summary>
        /// <param name="outputs">Outputs to add. 30 outputs is the maximum, 100 for donation</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> AddBatchOutputAsync(IEnumerable<IBatchOutputPost> outputs, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new AddBatchOutputRequest() { Outputs = outputs }, cancellationToken);
        }

    }
}
