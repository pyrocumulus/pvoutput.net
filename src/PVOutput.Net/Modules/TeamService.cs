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
    /// The Team service retrieves team information and joins/leaves teams.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsupply">API information</see>.</para>
    /// </summary>
    public sealed class TeamService : BaseService
    {
        internal TeamService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieves information for a team, that must be owned.
        /// <para><strong>Donation makes it possible to retrieve any team.</strong></para>
        /// </summary>
        /// <param name="teamId">Id of the team to retrieve information for. </param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Team information.</returns>
        public Task<PVOutputResponse<ITeam>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ITeam>(new TeamRequest { TeamId = teamId }, cancellationToken);
        }

        /// <summary>
        /// Adds a system to a team.
        /// </summary>
        /// <param name="teamId">Id of the team to join.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> JoinTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new JoinTeamRequest() { TeamId = teamId }, cancellationToken);
        }

        /// <summary>
        /// Removes a system from a team.
        /// </summary>
        /// <param name="teamId">Id of the team to leave.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        public Task<PVOutputBasicResponse> LeaveTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new LeaveTeamRequest() { TeamId = teamId }, cancellationToken);
        }
    }
}
