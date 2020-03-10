using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Search service retrieves a list of systems matching the given search query.
    /// <para>See the official <see href="https://pvoutput.org/help.html#api-search">API information</see>.</para>
    /// </summary>
    public sealed class SearchService : BaseService
    {
        internal SearchService(PVOutputClient client) : base(client)
        {
            
        }

        /// <summary>
        /// Retrieves a list of systems matching the provided query.
        /// <para>See <see href="https://pvoutput.org/help.html#search">this page</see> for help with the query syntax.</para>
        /// </summary>
        /// <param name="searchQuery">A search query to retrieve systems for.</param>
        /// <param name="coordinate">A GPS coordinate, used for distance queries.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchAsync(string searchQuery, PVCoordinate? coordinate = null, CancellationToken cancellationToken = default)
        {
            Guard.Argument(searchQuery, nameof(searchQuery)).NotEmpty().NotNull();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery, Coordinate = coordinate }, cancellationToken);
        }
    }
}
