using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <inheritdoc cref="INotificationService" />
    internal sealed class NotificationService : BaseService, INotificationService
    {
        /// <summary>
        /// Alert for a new private message.
        /// </summary>
        public const int PrivateMessageAlert = 1;

        /// <summary>
        /// Alert for joining a new team.
        /// </summary>
        public const int JoinedTeamAlert = 3;

        /// <summary>
        /// Alert for an added favourite.
        /// </summary>
        public const int AddedFavouriteAlert = 4;

        /// <summary>
        /// Alert for high consumption.
        /// </summary>
        public const int HighConsumptionAlert = 5;

        /// <summary>
        /// Alert for an idle system.
        /// </summary>
        public const int SystemIdleAlert = 6;

        /// <summary>
        /// Alert for low generation.
        /// </summary>
        public const int LowGenerationAlert = 8;

        /// <summary>
        /// Alert for low performance.
        /// </summary>
        public const int PerformanceAlert = 11;

        /// <summary>
        /// Alerty for a high standby cost alert.
        /// </summary>
        public const int StandbyCostAlert = 14;

        /// <summary>
        /// Alert for extended data element 7.
        /// </summary>
        public const int ExtendedDataV7Alert = 15;

        /// <summary>
        /// Alert for extended data element 8.
        /// </summary>
        public const int ExtendedDataV8Alert = 16;

        /// <summary>
        /// Alert for extended data element 9.
        /// </summary>
        public const int ExtendedDataV9Alert = 17;

        /// <summary>
        /// Alert for extended data element 10.
        /// </summary>
        public const int ExtendedDataV10Alert = 18;

        /// <summary>
        /// Alert for extended data element 11.
        /// </summary>
        public const int ExtendedDataV11Alert = 19;

        /// <summary>
        /// Alert for extended data element 12.
        /// </summary>
        public const int ExtendedDataV12Alert = 20;

        /// <summary>
        /// Alert for high net power.
        /// </summary>
        public const int HighNetPowerAlert = 23;

        /// <summary>
        /// Alert for low net power.
        /// </summary>
        public const int LowNetPowerAlert = 24;

        internal NotificationService(PVOutputClient client) : base(client)
        {
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> RegisterNotificationAsync(string applicationId, string callbackUrl, int? alertType = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.NotificationService_RegisterNotification,
                [LoggingEvents.Parameter_ApplicationId] = applicationId,
                [LoggingEvents.Parameter_CallBackUrl] = callbackUrl
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_AlertType, alertType);

            Guard.IsNotNullOrEmpty(applicationId, nameof(applicationId));
            Guard.HasSizeLessThanOrEqualTo(applicationId, 100, nameof(applicationId));
            Guard.IsNotNullOrEmpty(callbackUrl, nameof(callbackUrl));
            Guard.HasSizeLessThanOrEqualTo(callbackUrl, 150, nameof(callbackUrl));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new RegisterNotificationRequest() { ApplicationId = applicationId, CallbackUri = new Uri(callbackUrl), AlertType = alertType }, loggingScope, cancellationToken);
        }


        /// <inheritdoc />
        public Task<PVOutputBasicResponse> RegisterNotificationAsync(string applicationId, Uri callbackUri, int? alertType = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.NotificationService_RegisterNotification,
                [LoggingEvents.Parameter_ApplicationId] = applicationId,
                [LoggingEvents.Parameter_CallBackUrl] = callbackUri.AbsoluteUri
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_AlertType, alertType);

            Guard.IsNotNullOrEmpty(applicationId, nameof(applicationId));
            Guard.HasSizeLessThanOrEqualTo(applicationId, 100, nameof(applicationId));
            Guard.IsNotNull(callbackUri, nameof(callbackUri));
            Guard.IsNotNullOrEmpty(callbackUri.AbsoluteUri, nameof(callbackUri));
            Guard.HasSizeLessThanOrEqualTo(callbackUri.AbsoluteUri, 150, nameof(callbackUri));

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new RegisterNotificationRequest() { ApplicationId = applicationId, CallbackUri = callbackUri, AlertType = alertType }, loggingScope, cancellationToken);
        }

        /// <inheritdoc />
        public Task<PVOutputBasicResponse> DeregisterNotificationAsync(string applicationId, int? alertType = null, CancellationToken cancellationToken = default)
        {
            var loggingScope = new Dictionary<string, object>()
            {
                [LoggingEvents.RequestId] = LoggingEvents.NotificationService_DeregisterNotification,
                [LoggingEvents.Parameter_ApplicationId] = applicationId
            };
            loggingScope.AddIfNotNull(LoggingEvents.Parameter_AlertType, alertType);

            if (!string.IsNullOrEmpty(applicationId))
            {
                Guard.HasSizeLessThanOrEqualTo(applicationId, 100, nameof(applicationId));
            }

            var handler = new RequestHandler(Client);
            return handler.ExecutePostRequestAsync(new DeregisterNotificationRequest { ApplicationId = applicationId!, AlertType = alertType }, loggingScope, cancellationToken);
        }
    }
}
