using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Team service retrieves team information and joins/leaves teams.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-team-service">API information</see>.</para>
    /// </summary>
    public interface ITeamService
    {
        /// <summary>
        /// Retrieves information for a team, that must be owned.
        /// <para><strong>Donation makes it possible to retrieve any team.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-team-service">API information</see>.</para>
        /// </summary>
        /// <param name="teamId">Id of the team to retrieve information for. </param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team information.</returns>
        Task<PVOutputResponse<ITeam>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a system to a team.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#join-team-service">API information</see>.</para>
        /// </summary>
        /// <param name="teamId">Id of the team to join.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> JoinTeamAsync(int teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a system from a team.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#leave-team-service">API information</see>.</para>
        /// </summary>
        /// <param name="teamId">Id of the team to leave.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> LeaveTeamAsync(int teamId, CancellationToken cancellationToken = default);
    }
}
