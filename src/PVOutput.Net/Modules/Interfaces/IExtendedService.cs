using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Extended service retrieves system daily extended data.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-extended-service">API information</see>.</para>
    /// <para><strong>Note: this is a donation only service.</strong></para>
    /// </summary>
    public interface IExtendedService
    {
        /// <summary>
        /// Retrieve most recently created extended data.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-extended-service">API information</see>.</para>
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>Most recent extended data.</returns>
        Task<PVOutputResponse<IExtended>> GetRecentExtendedDataAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve extended data for a certain period.
        /// <para><strong>Note: this is a donation only request.</strong></para>
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-extended-service">API information</see>.</para>
        /// </summary>
        /// <param name="fromDate">Minimum DateTime for the requested range.</param>
        /// <param name="toDate">Maximum DateTime for the requested range.</param>
        /// <param name="limit">Optional limit of number of items. <c>50 is the maximum.</c></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of extended data objects.</returns>
        Task<PVOutputArrayResponse<IExtended>> GetExtendedDataForPeriodAsync(DateTime fromDate, DateTime toDate, int? limit = null, CancellationToken cancellationToken = default);
    }
}
