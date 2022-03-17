using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <inheritdoc cref="ISearchService"/>
    internal sealed class SearchService : BaseService, ISearchService
    {
        internal SearchService(PVOutputClient client) : base(client)
        {
            
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
