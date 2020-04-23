﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Builders;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get System service retrieves system information.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsystem">API information</see>.</para>
    /// </summary>
    public sealed class SystemService : BaseService
    {
        internal SystemService(PVOutputClient client) : base(client)
        {
        }

        /// <summary>
        /// Retrieves information for the owned system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the owned system.</returns>
        public Task<PVOutputResponse<ISystem>> GetOwnSystemAsync(CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SystemService_GetOwnSystem

            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteSingleItemRequestAsync<ISystem>(new SystemRequest(), loggingScope, cancellationToken);
        }

        /// <summary>
        /// Retrieves information for a system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="systemId">Id of the system to request information for</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the requested system.</returns>
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

        /// <summary>
        /// Updates a system's name or extended data values. 
        /// Use the <see cref="ExtendedDataDefinitionBuilder"/> to create definition for extended data values.
        /// <para>See the official <see href="https://pvoutput.org/help.html#api-postsystem">API information</see>.</para>
        /// </summary>
        /// <param name="systemId">The system to update.</param>
        /// <param name="systemName">A new name for the system.</param>
        /// <param name="dataDefinitions">List of modified extended data definitions.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
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
