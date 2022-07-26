using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Builders;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    ///<summary>
    /// The Output service handles daily output information for individual systems.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-output-service">API information</see>.</para>
    /// </summary>
    public interface IOutputService
    {
        /// <summary>
        /// Retrieve daily output for a date.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-output-service">API information</see>.</para>
        /// </summary>
        /// <param name="outputDate">Date to retrieve the output for.</param>
        /// <param name="getInsolation">Also retrieve insolation information. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="systemId">Retrieve output for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Output for the requested date.</returns>
        Task<PVOutputResponse<IOutput>> GetOutputForDateAsync(DateTime outputDate, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve daily outputs for a period.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-output-service">API information</see>.</para>
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="getInsolation">Also retrieve insolation information. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="systemId">Retrieve output for a specific system. <strong>Note: this is a donation only parameter.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Outputs for the requested period.</returns>
        Task<PVOutputArrayResponse<IOutput>> GetOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, bool getInsolation = false, int? systemId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve daily output for a team.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#team-output">API information</see>.</para>
        /// </summary>
        /// <param name="outputDate">Date to retrieve the output for.</param>
        /// <param name="teamId">Team to retrieve the output for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team output for the requested date.</returns>
        Task<PVOutputResponse<ITeamOutput>> GetTeamOutputForDateAsync(DateTime outputDate, int teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve daily team outputs for a period.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#team-output">API information</see>.</para>
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="teamId">Team to retrieve the output for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team outputs Outputs for the requested period.</returns>
        Task<PVOutputArrayResponse<ITeamOutput>> GetTeamOutputsForPeriodAsync(DateTime fromDate, DateTime toDate, int teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve aggregated outputs for a period.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#aggregated-outputs">API information</see>.</para>
        /// </summary>
        /// <param name="fromDate">Minimum DateTime for the requested range.</param>
        /// <param name="toDate">Maximum DateTime for the requested range.</param>
        /// <param name="period">Period to aggregate the outputs in.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Aggregated outputs for the requested period.</returns>
        Task<PVOutputArrayResponse<IAggregatedOutput>> GetAggregatedOutputsAsync(DateTime fromDate, DateTime toDate, AggregationPeriod period, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a single daily output to the owned system.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#add-output-service">API information</see>.</para>
        /// Use the <see cref="OutputPostBuilder"/> to create <see cref="IOutputPost"/> objects.
        /// </summary>
        /// <param name="output">The output to add.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> AddOutputAsync(IOutputPost output, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a list of outputs to the owned system. <strong>Note: this is a donation only function.</strong>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#add-output-service">API information</see>.</para>
        /// Use the <see cref="OutputPostBuilder"/> to create <see cref="IOutputPost"/> objects.
        /// </summary>
        /// <param name="outputs">Outputs to add</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> AddOutputsAsync(IEnumerable<IOutputPost> outputs, CancellationToken cancellationToken = default);
    }
}
