using System;
using System.Collections.Generic;
using System.Text;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder that creates statuses of type <typeparamref name="TResultType"/> to post to PVOutput.
    /// </summary>
    /// <typeparam name="TResultType">The status type to post.</typeparam>
    public sealed class StatusPostBuilder<TResultType> where TResultType : class, IBatchStatusPost
    {
        internal StatusPost _statusPost;

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public StatusPostBuilder()
        {
            _statusPost = new StatusPost();
        }

        /// <summary>
        /// Sets the timestamp for the status.
        /// </summary>
        /// <param name="timestamp">Timestamp.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetTimeStamp(DateTime timestamp)
        {
            Guard.Argument(timestamp, nameof(timestamp)).IsNoFutureDate();
            
            _statusPost.Timestamp = timestamp;
            return this;
        
        }

        /// <summary>
        /// Sets generation information for the status.
        /// </summary>
        /// <param name="energyGeneration">Total energy generated up to and including timestamp.</param>
        /// <param name="powerGeneration">Actual power being generated at the moment of the timestamp.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetGeneration(int? energyGeneration, int? powerGeneration = null)
        {
            Guard.Argument(energyGeneration, nameof(energyGeneration)).Min(0);
            Guard.Argument(powerGeneration, nameof(powerGeneration)).Min(0);

            _statusPost.EnergyGeneration = energyGeneration;
            _statusPost.PowerGeneration = powerGeneration;
            return this;
        }

        /// <summary>
        /// Sets consumption information for the status.
        /// </summary>
        /// <param name="energyConsumption">Total energy consumed up to and including the timestamp.</param>
        /// <param name="powerConsumption">Actual power being consumed at the moment of the timestamp.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetConsumption(int? energyConsumption, int? powerConsumption = null)
        {
            Guard.Argument(energyConsumption, nameof(energyConsumption)).Min(0);
            Guard.Argument(powerConsumption, nameof(powerConsumption)).Min(0);

            _statusPost.EnergyConsumption = energyConsumption;
            _statusPost.PowerConsumption = powerConsumption;
            return this;
        }

        /// <summary>
        /// Sets the current temperature for the status.
        /// </summary>
        /// <param name="temperature">Actual temperature.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetTemperature(decimal temperature)
        {
            _statusPost.Temperature = temperature;
            return this;
        }

        /// <summary>
        /// Sets the measured voltage for the status.
        /// </summary>
        /// <param name="voltage">Recorded voltage.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetVoltage(decimal voltage)
        {
            Guard.Argument(voltage, nameof(voltage)).InRange(0, 300);

            _statusPost.Voltage = voltage;
            return this;
        }

        /// <summary>
        /// Sets the cumulative type, if any, for the status.
        /// </summary>
        /// <param name="type">Type of cumulative for the status.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetCumulativeType(CumulativeStatusType type)
        {
            _statusPost.Cumulative = type;
            return this;
        }

        /// <summary>
        /// Sets whether or not the power values are net export/import rather than gross.
        /// </summary>
        /// <param name="net">Net indication.</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> IsNetValue(bool net = true)
        {
            _statusPost.Net = net;
            return this;
        }

        /// <summary>
        /// Sets the extended values for the status.
        /// </summary>
        /// <param name="value1">Extended value 1</param>
        /// <param name="value2">Extended value 2</param>
        /// <param name="value3">Extended value 3</param>
        /// <param name="value4">Extended value 4</param>
        /// <param name="value5">Extended value 5</param>
        /// <param name="value6">Extended value 6</param>
        /// <returns>The builder.</returns>
        public StatusPostBuilder<TResultType> SetExtendedValues(decimal? value1, decimal? value2 = null, decimal? value3 = null, decimal? value4 = null, decimal? value5 = null, decimal? value6 = null)
        {
            _statusPost.ExtendedValue1 = value1;
            _statusPost.ExtendedValue2 = value2;
            _statusPost.ExtendedValue3 = value3;
            _statusPost.ExtendedValue4 = value4;
            _statusPost.ExtendedValue5 = value5;
            _statusPost.ExtendedValue6 = value6;
            return this;
        }

        /// <summary>
        /// A text message to record with the status.
        /// </summary>
        /// <param name="textMessage">Text message.</param>
        /// <returns>The builder.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        public StatusPostBuilder<TResultType> SetTextMessage(string textMessage)
        {
            Guard.Argument(textMessage, nameof(textMessage)).NotEmpty().LengthInRange(1, 30);

            _statusPost.TextMessage = textMessage;
            return this;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new status.
        /// </summary>
        public void Reset()
        {
            _statusPost = new StatusPost();
        }

        /// <summary>
        /// Uses information within the builder to return the built status.
        /// </summary>
        /// <returns>The status <typeparamref name="TResultType"/>.</returns>
        public TResultType Build()
        {
            ValidateStatus();
            
            return _statusPost as TResultType;
        }

        /// <summary>
        /// Uses information within the builder to return the built status.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The status <typeparamref name="TResultType"/>.</returns>
        public TResultType BuildAndReset()
        {
            TResultType result = Build();
            Reset();
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        private void ValidateStatus()
        {
            if (_statusPost.EnergyGeneration == null 
                && _statusPost.PowerGeneration == null
                && _statusPost.EnergyConsumption == null 
                && _statusPost.PowerConsumption == null)
            {
                throw new InvalidOperationException("Status has no generation or consumption values");
            }
        }
    }
}
