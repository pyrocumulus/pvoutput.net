using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="ISystemService"/>>
    public sealed class SystemService : BaseService, ISystemService
    {
        internal SystemService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<ISystem>> GetOwnSystemAsync(CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SystemService_GetOwnSystem

            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest(), loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputResponse<ISystem>> GetOtherSystemAsync(int systemId, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SystemService_GetOtherSystem,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest { SystemId = systemId, MonthlyEstimates = false }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> PostSystem(int systemId, string systemName = null, IEnumerable<IExtendedDataDefinition> dataDefinitions = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SystemService_PostSystem,
                [LoggingEvents.Parameter_SystemId] = systemId
            };

            Guard.Argument(systemName).MaxLength(30);

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new PostSystemRequest() { SystemId = systemId, SystemName = systemName, DataDefinitions = dataDefinitions }, loggingScope, cancellationToken);
        }
    }
}
