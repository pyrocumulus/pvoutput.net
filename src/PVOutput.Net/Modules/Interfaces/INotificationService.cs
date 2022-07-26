using System;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    /// <summary>
    /// The Notification service enables (de-)registering alert notifications.
    /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#register-notification-service">API information</see>.</para>
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Registers an application for a notification.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#register-notification-service">API information</see>.</para>
        /// </summary>
        /// <param name="applicationId">ApplicationId to register the notification under.</param>
        /// <param name="callbackUrl">The url that should get called for each notification.</param>
        /// <param name="alertType">A specific type of alert to send, leave empty for all alert types.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> RegisterNotificationAsync(string applicationId, string callbackUrl, int? alertType = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Registers an application for a notification.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#register-notification-service">API information</see>.</para>
        /// </summary>
        /// <param name="applicationId">ApplicationId to register the notification under.</param>
        /// <param name="callbackUri">The uri that should get called for each notification.</param>
        /// <param name="alertType">A specific type of alert to send, leave empty for all alert types.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> RegisterNotificationAsync(string applicationId, Uri callbackUri, int? alertType = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deregisters an application for a notification.
        /// <para>See the official <see href="https://pvoutput.org/help/api_specification.html#deregister-notification-service">API information</see>.</para>
        /// </summary>
        /// <param name="applicationId">ApplicationId to register the notification under.</param>
        /// <param name="alertType">A specific type of alert to send, leave empty for all alert types.</param>
        /// <param name="cancellationToken">A cancellation token for the request.</param>
        /// <returns>If the operation succeeded.</returns>
        Task<PVOutputBasicResponse> DeregisterNotificationAsync(string applicationId, int? alertType = null, CancellationToken cancellationToken = default);
    }
}
