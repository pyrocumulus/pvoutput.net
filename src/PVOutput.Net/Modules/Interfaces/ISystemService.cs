using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Builders;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get System service retrieves system information.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getsystem">API information</see>.</para>
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// Retrieves information for the owned system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the owned system.</returns>
        Task<PVOutputResponse<ISystem>> GetOwnSystemAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves information for a system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// </summary>
        /// <param name="systemId">Id of the system to request information for</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Information of the requested system.</returns>
        Task<PVOutputResponse<ISystem>> GetOtherSystemAsync(int systemId, CancellationToken cancellationToken = default);

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
        Task<PVOutputBasicResponse> PostSystem(int systemId, string systemName = null, IEnumerable<IExtendedDataDefinition> dataDefinitions = null, CancellationToken cancellationToken = default);
    }
}
