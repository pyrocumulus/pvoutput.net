﻿using PVOutput.Net.Modules;

namespace PVOutput.Net
{
    /// <summary>
    /// The client used to communicate with the PVOutput service.
    /// </summary>
    public interface IPVOutputClient
    {
        /// <summary>ApiKey to use with authenticating.</summary>
        string ApiKey { get; set; }

        /// <summary>Id of the currently owned system used for authenticating.</summary>
        int OwnedSystemId { get; set; }

        /// <summary>Indicates whether or not the client should throw exceptions when the api returns errors.</summary>
        bool ThrowResponseExceptions { get; set; }

        /// <summary>
        /// The output service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-output-service">API information</see>.</para>
        /// </summary>
        IOutputService Output { get; }

        /// <summary>
        /// The system service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-system-service">API information</see>.</para>
        /// </summary>
        ISystemService System { get; }

        /// <summary>
        /// The status service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-status-service">API information</see>.</para>
        /// </summary>
        IStatusService Status { get; }

        /// <summary>
        /// The statistic service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-statistic-service">API information</see>.</para>
        /// </summary>
        IStatisticsService Statistics { get; }

        /// <summary>
        /// The missing service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-missing-service">API information</see>.</para>
        /// </summary>
        IMissingService Missing { get; }

        /// <summary>
        /// The team service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-supply-service">API information</see>.</para>
        /// </summary>
        ITeamService Team { get; }

        /// <summary>
        /// The extended service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-extended-service">API information</see>.</para>
        /// </summary>
        IExtendedService Extended { get; }

        /// <summary>
        /// The favourite service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-favourite-service">API information</see>.</para>
        /// </summary>
        IFavouriteService Favourite { get; }

        /// <summary>
        /// The insolation service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-insolation-service">API information</see>.</para>
        /// </summary>
        IInsolationService Insolation { get; }

        /// <summary>
        /// The supply service.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#get-supply-service">API information</see>.</para>
        /// </summary>
        ISupplyService Supply { get; }

        /// <summary>
        /// The search service
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#search-service">API information</see>.</para>
        /// </summary>
        ISearchService Search { get; }

        /// <summary>
        /// The notification service
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#register-notification-service">API information</see>.</para>
        /// </summary>
        INotificationService Notification { get; }
    }
}
