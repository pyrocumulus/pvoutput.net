using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Get Favourite service retrieves the systems in your favourites or another user's favourites.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-getfavourite">API information</see>.</para>
    /// </summary>
    public interface IFavouriteService
    {
        /// <summary>
        /// Retrieves the favourites for the owned system, or another.
        /// </summary>
        /// <param name="systemId">Optional SystemId to request the favourites for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>List of favourites.</returns>
        Task<PVOutputArrayResponse<IFavourite>> GetFavouritesAsync(int? systemId = null, CancellationToken cancellationToken = default);
    }
}
