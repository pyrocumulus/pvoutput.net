﻿using System;
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
    public class TeamService : BaseService
    {
        public TeamService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputResponse<ITeam>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ITeam>(new TeamRequest { TeamId = teamId }, cancellationToken);
        }
    }
}
