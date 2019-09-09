using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Modules
{
    public abstract class BaseService
    {
        public PVOutputClient Client { get; }

        public BaseService(PVOutputClient client)
        {
            Client = client;
        }
    }
}
