using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Insolation service retrieves 5-minute insolation data (power and energy) under ideal weather conditions.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-insolation-service">API information</see>.</para>
    /// <para><strong>Note: this is a donation only service.</strong></para>
    /// </summary>
    public interface IInsolationService
    {
        /// <summary>
        /// Get insolation data for own system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-insolation-service">API information</see>.</para>
        /// </summary>
        /// <param name="insolationDate">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the owned system.</returns>
        Task<PVOutputArrayResponse<IInsolation>> GetInsolationForOwnSystemAsync(DateTime? insolationDate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get insolation data for a system.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-insolation-service">API information</see>.</para>
        /// </summary>
        /// <param name="systemId">Id of the system to get insolation for.</param>
        /// <param name="insolationDate">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the requested system.</returns>
        Task<PVOutputArrayResponse<IInsolation>> GetInsolationForSystemAsync(int systemId, DateTime? insolationDate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get insolation data for a location.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-insolation-service">API information</see>.</para>
        /// </summary>
        /// <param name="coordinate">GPS coordinate, to request insolation for.</param>
        /// <param name="insolationDate">The DateTime to calculate the insolation for. If empty, the current date will be calculated.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Insolation data for the requested location.</returns>
        Task<PVOutputArrayResponse<IInsolation>> GetInsolationForLocationAsync(PVCoordinate coordinate, DateTime? insolationDate = null, CancellationToken cancellationToken = default);
    }
}
