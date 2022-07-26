using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Supply service displays aggregated live generation and consumption data.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-supply-service">API information</see>.</para>
    /// </summary>
    public interface ISupplyService
    {
        /// <summary>
        /// Retrieves live generation and consumption data
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-supply-service">API information</see>.</para>
        /// </summary>
        /// <param name="timeZone">A timezone identifier in the 'Europe/London' format. Will default to GMT+0 if empty.</param>
        /// <param name="regionKey">Region key in the '1:queensland' format. <strong>Donation only option.</strong></param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of supply information</returns>
        Task<PVOutputArrayResponse<ISupply>> GetSupplyAsync(string timeZone = null, string regionKey = null, CancellationToken cancellationToken = default);
    }
}
