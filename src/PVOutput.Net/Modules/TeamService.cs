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
    /// <inheritdoc cref="ITeamService"/>
    internal sealed class TeamService : BaseService, ITeamService
    {
        internal TeamService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<ITeam>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.TeamService_GetTeam,
                [LoggingEvents.Parameter_TeamId] = teamId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ITeam>(new TeamRequest { TeamId = teamId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> JoinTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.TeamService_JoinTeam,
                [LoggingEvents.Parameter_TeamId] = teamId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new JoinTeamRequest() { TeamId = teamId }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> LeaveTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.TeamService_LeaveTeam,
                [LoggingEvents.Parameter_TeamId] = teamId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new LeaveTeamRequest() { TeamId = teamId }, loggingScope, cancellationToken);
        }
    }
}
