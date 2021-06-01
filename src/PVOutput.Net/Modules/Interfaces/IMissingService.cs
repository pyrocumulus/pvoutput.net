using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Missing service retrieves a list of output dates missing from a system.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getmissing">API information</see>.</para>
    /// </summary>
    public interface IMissingService
    {
        /// <summary>
        /// Retrieves a list of output dates with no data
        /// </summary>
        /// <param name="fromDate">Minimum date for the requested range.</param>
        /// <param name="toDate">Maximum date for the requested range.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of missing dates</returns>
        Task<PVOutputResponse<IMissing>> GetMissingDaysInPeriodAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    }
}
