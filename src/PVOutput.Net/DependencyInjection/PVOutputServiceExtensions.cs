using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace PVOutput.Net.DependencyInjection
{
    /// <summary>
    /// Extensions to enable adding a PVOutput client through Microsoft's Dependency Injection.
    /// </summary>
    public static class PVOutputServiceExtensions
    {
        /// <summary>
        /// Add a PVOutputClient as a singleton client to a IServiceCollection.
        /// </summary>
        /// <param name="services">The servicecollection to add the client to.</param>
        /// <param name="optionsAction">An action to configure the provided options.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        public static void AddPVOutputClient(this IServiceCollection services, Action<PVOutputClientOptions> optionsAction)
        {
            if (optionsAction == null)
            {
                throw new ArgumentNullException(nameof(optionsAction), "Please provide options to the PVOutputClient.");
            }

            var options = new PVOutputClientOptions();
            optionsAction.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<PVOutputClient>();
        }
    }
}
