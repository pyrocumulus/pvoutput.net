using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
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
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_Search,
                [LoggingEvents.Parameter_SearchQueryText] = searchQuery,
                [LoggingEvents.Parameter_Coordinate] = coordinate
            };
            
            Guard.Argument(searchQuery, nameof(searchQuery)).NotEmpty().NotNull();

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery, Coordinate = coordinate }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by name.
        /// </summary>
        /// <param name="name">Name to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="name"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByNameAsync(string name, bool useStartsWith = true, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByName,
                [LoggingEvents.Parameter_Search_Name] = name,
                [LoggingEvents.Parameter_Search_UseStartsWith] = useStartsWith
            };

            Guard.Argument(name, nameof(name)).NotEmpty();

            string query = FormatStartsWith(name, useStartsWith);

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = query }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems that have either a postcode or total size that begins with a value.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPostcodeOrSizeAsync(int value, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByPostCodeOrSize,
                [LoggingEvents.Parameter_Search_Value] = value
            };

            Guard.Argument(value, nameof(value)).GreaterThan(0);
            string query = value.ToString("#####", CultureInfo.InvariantCulture);

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = query }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by postcode.
        /// </summary>
        /// <param name="postcode">Postcode to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPostcodeAsync(string postcode, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByPostCode,
                [LoggingEvents.Parameter_Search_Postcode] = postcode
            };

            Guard.Argument(postcode, nameof(postcode)).NotEmpty();

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(postcode, "postcode");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by size.
        /// </summary>
        /// <param name="value">Size to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchBySizeAsync(int value, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchBySize,
                [LoggingEvents.Parameter_Search_Value] = value
            };

            Guard.Argument(value, nameof(value)).GreaterThan(0);

            string query = value.ToString("#####", CultureInfo.InvariantCulture);

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(query, "size");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by panel.
        /// </summary>
        /// <param name="panel">Panel to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByPanelAsync(string panel, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByPanel,
                [LoggingEvents.Parameter_Search_Panel] = panel
            };

            Guard.Argument(panel, nameof(panel)).NotEmpty();

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(panel, "panel");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by inverter.
        /// </summary>
        /// <param name="inverter">Inverter to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="inverter"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByInverterAsync(string inverter, bool useStartsWith = true, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByInverter,
                [LoggingEvents.Parameter_Search_Inverter] = inverter
            };

            Guard.Argument(inverter, nameof(inverter)).NotEmpty();
            var query = FormatStartsWith(inverter, useStartsWith);

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(query, "inverter");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by distance.
        /// </summary>
        /// <param name="postcode">Postcode to search from.</param>
        /// <param name="kilometers">Distance to search with <c>(25 is the maximum)</c>.</param>
        /// <param name="countryCode">Country code of the postcode to search in <c>(eg. "nl")</c>.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByDistanceAsync(int postcode, int kilometers, string countryCode, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByDistance,
                [LoggingEvents.Parameter_Search_Postcode] = postcode,
                [LoggingEvents.Parameter_Search_Kilometers] = kilometers,
                [LoggingEvents.Parameter_Search_CountryCode] = countryCode
            };

            Guard.Argument(postcode, nameof(postcode)).GreaterThan(0);
            Guard.Argument(kilometers, nameof(kilometers)).InRange(1, 25);
            Guard.Argument(countryCode, nameof(countryCode)).NotEmpty().Length(2);

            string query = $"{postcode:####} {kilometers:##}km";
            
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = query, CountryCode = countryCode }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by distance.
        /// </summary>
        /// <param name="coordinate">GPS coordinate to search from.</param>
        /// <param name="kilometers">Distance to search with <c>(25 is the maximum)</c>.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByDistanceAsync(PVCoordinate coordinate, int kilometers, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByDistance,
                [LoggingEvents.Parameter_Search_Kilometers] = kilometers,
                [LoggingEvents.Parameter_Coordinate] = coordinate
            };

            Guard.Argument(kilometers, nameof(kilometers)).InRange(1, 25);

            string query = $"{kilometers:##}km";

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = query, Coordinate = coordinate }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by team name.
        /// </summary>
        /// <param name="teamName">Team name to search for.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByTeamAsync(string teamName, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByTeam,
                [LoggingEvents.Parameter_Search_TeamName] = teamName
            };

            Guard.Argument(teamName, nameof(teamName)).NotEmpty();

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(teamName, "team");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by orientation.
        /// </summary>
        /// <param name="orientation">Orientation to search for.</param>
        /// <param name="name">Name to search for.</param>
        /// <param name="useStartsWith">If <c>true</c> the name should start with the <paramref name="name"/> value. Otherwise the search is performed using a <c>contains</c> method.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByOrientationAsync(Orientation orientation, string name = null, bool useStartsWith = true, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByOrientation,
                [LoggingEvents.Parameter_Search_Orientation] = orientation,
                [LoggingEvents.Parameter_Search_Name] = name,
                [LoggingEvents.Parameter_Search_UseStartsWith] = useStartsWith
            };

            string query = "";
            if (name != null)
            {
                query = FormatStartsWith(name, useStartsWith);
            }

            query += $" %2B{FormatHelper.GetEnumerationDescription(orientation)}";
            
            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = query }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Search for systems by tilt.
        /// </summary>
        /// <param name="tilt">Tilt to search for (should be within 2.5 degrees of this value).</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> SearchByTiltAsync(int tilt, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_SearchByTilt,
                [LoggingEvents.Parameter_Search_Tilt] = tilt
            };

            Guard.Argument(tilt, nameof(tilt)).GreaterThan(0);

            string query = tilt.ToString("###", CultureInfo.InvariantCulture);

            var handler = new RequestHandler(Client);
            var searchQuery = CreateQueryWithKeyword(query, "tilt");
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = searchQuery }, loggingScope, cancellationToken);
        }

        /// <summary>
        /// Retrieve your favourite systems.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>A list of search results.</returns>
        public Task<PVOutputArrayResponse<ISystemSearchResult>> GetFavouritesAsync(CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.SearchService_GetFavourites
            };

            var handler = new RequestHandler(Client);
            return handler.ExecuteArrayRequestAsync<ISystemSearchResult>(new SearchRequest { SearchQuery = "favourite" }, loggingScope, cancellationToken);
        }

        private static string FormatStartsWith(string query, bool useStartsWith)
        {
            if (useStartsWith && !query.EndsWith("%2A", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"{query}%2A";
            }

            return query.Replace("*", "%2A");
        }

        private static string CreateQueryWithKeyword(string queryText, string keyword)
        {
            string searchQuery = queryText;
            if (!string.IsNullOrEmpty(keyword))
            {
                searchQuery = $"{queryText} {keyword}".Trim();
            }

            return searchQuery;
        }
    }
}
