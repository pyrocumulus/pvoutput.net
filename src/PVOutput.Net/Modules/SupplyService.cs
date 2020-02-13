﻿using System;
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
    public sealed class SupplyService : BaseService
    {
        public SupplyService(PVOutputClient client) : base(client)
        {

        }

        public Task<PVOutputArrayResponse<ISupply>> GetSupplyAsync(string timeZone = null, string regionKey = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);

            return handler.ExecuteArrayRequestAsync<ISupply>(
                new SupplyRequest { TimeZone = timeZone, RegionKey = regionKey }, cancellationToken);
        }
    }
}