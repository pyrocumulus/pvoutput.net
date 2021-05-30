using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Search service retrieves a list of systems matching the given search query.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-search">API information</see>.</para>
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Retrieves a list of systems matching the provided query.
        /// <para>See <see href="https://pvoutput.org/help.html#search">this page</see> for help with the query syntax.</para>
        /// </summary>
        /// <param name="searchQuery">A search query to retrieve systems for.</param>
        /// <param name="coordinate">A GPS coordinate, used for distance queries.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchAsync(string searchQuery, PVCoordinate? coordinate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by name.
        /// </summary>
        /// <param name="name">Name to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="name"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByNameAsync(string name, bool useStartsWith = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems that have either a postcode or total size that begins with a value.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPostcodeOrSizeAsync(int value, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by postcode.
        /// </summary>
        /// <param name="postcode">Postcode to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPostcodeAsync(string postcode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by size.
        /// </summary>
        /// <param name="value">Size to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchBySizeAsync(int value, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by panel.
        /// </summary>
        /// <param name="panel">Panel to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPanelAsync(string panel, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by inverter.
        /// </summary>
        /// <param name="inverter">Inverter to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="inverter"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByInverterAsync(string inverter, bool useStartsWith = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by distance.
        /// </summary>
        /// <param name="postcode">Postcode to search from.</param>
        /// <param name="kilometers">Distance to search with <c>(25 is the maximum)</c>.</param>
        /// <param name="countryCode">Country code of the postcode to search in <c>(eg. "nl")</c>.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByDistanceAsync(int postcode, int kilometers, string countryCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by distance.
        /// </summary>
        /// <param name="coordinate">GPS coordinate to search from.</param>
        /// <param name="kilometers">Distance to search with <c>(25 is the maximum)</c>.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByDistanceAsync(PVCoordinate coordinate, int kilometers, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by team name.
        /// </summary>
        /// <param name="teamName">Team name to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByTeamAsync(string teamName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by orientation.
        /// </summary>
        /// <param name="orientation">Orientation to search for.</param>
        /// <param name="name">Name to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="name"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByOrientationAsync(Orientation orientation, string name = null, bool useStartsWith = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for systems by tilt.
        /// </summary>
        /// <param name="tilt">Tilt to search for (should be within 2.5 degrees of this value).</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByTiltAsync(int tilt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve your favourite systems.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        Task<PVOutputArrayResponse<ISystemSearchResult>> GetFavouritesAsync(CancellationToken cancellationToken = default);
    }
}
