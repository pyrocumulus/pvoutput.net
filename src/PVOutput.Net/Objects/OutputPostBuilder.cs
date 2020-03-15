using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Builder that creates outputs of type <typeparamref name="TResultType"/> to post to PVOutput.
    /// </summary>
    /// <typeparam name="TResultType">The output type to post.</typeparam>
    public sealed class OutputPostBuilder<TResultType> where TResultType : class, IBatchOutputPost
    {
        internal OutputPost _outputPost;

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public OutputPostBuilder()
        {
            _outputPost = new OutputPost();
        }

        /// <summary>
        /// Sets the date for the output for the output.
        /// </summary>
        /// <param name="date">The date to set.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetDate(DateTime date)
        {
            Guard.Argument(date, nameof(date)).IsNoFutureDate().NoTimeComponent();

            _outputPost.OutputDate = date;
            return this;
        }

        /// <summary>
        /// Sets the total energy generated for the output.
        /// </summary>
        /// <param name="energyGenerated">Total energy generated.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetGenerated(int energyGenerated)
        {
            Guard.Argument(energyGenerated, nameof(energyGenerated)).Min(0);

            _outputPost.EnergyGenerated = energyGenerated;
            return this;
        }

        /// <summary>
        /// Sets the total energy exported for the output.
        /// </summary>
        /// <param name="energyExported">Total energy exported.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetExported(int energyExported)
        {
            Guard.Argument(energyExported, nameof(energyExported)).Min(0);

            _outputPost.EnergyExported = energyExported;
            return this;
        }

        /// <summary>
        /// Sets the time at which peak power was recorded.
        /// </summary>
        /// <param name="peakTime">Peak time.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetPeakTime(DateTime peakTime)
        {
            _outputPost.PeakTime = peakTime;
            return this;
        }

        /// <summary>
        /// Sets the peak power for the output.
        /// </summary>
        /// <param name="peakPower">Peak power.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetPeakPower(int peakPower)
        {
            Guard.Argument(peakPower, nameof(peakPower)).Min(0);

            _outputPost.PeakPower = peakPower;
            return this;
        }

        /// <summary>
        /// Sets the condition for the output.
        /// </summary>
        /// <param name="condition">The weather conditions on the output day.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetCondition(WeatherCondition condition)
        {
            _outputPost.Condition = condition;
            return this;
        }

        /// <summary>
        /// Sets the minimum and maximum temperatures for the output.
        /// </summary>
        /// <param name="minimumTemperature">Minimum recorded temperature.</param>
        /// <param name="maximumTemperature">Maximum recorded temperature.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetTemperatures(decimal? minimumTemperature, decimal? maximumTemperature)
        {
            Guard.NotAllNull(Guard.Argument(minimumTemperature, nameof(minimumTemperature)), Guard.Argument(maximumTemperature, nameof(maximumTemperature)));

            if (minimumTemperature.HasValue && maximumTemperature.HasValue)
            {
                Guard.Argument(maximumTemperature.Value, nameof(maximumTemperature)).GreaterThan(minimumTemperature.Value);
            }

            _outputPost.MinimumTemperature = minimumTemperature;
            _outputPost.MaximumTemperature = maximumTemperature;
            return this;
        }

        /// <summary>
        /// Sets comments for the output.
        /// </summary>
        /// <param name="comments">Comments.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetComments(string comments)
        {
            Guard.Argument(comments, nameof(comments)).NotEmpty().NotNull();

            _outputPost.Comments = comments;
            return this;
        }

        /// <summary>
        /// Sets peak energy import for the output.
        /// </summary>
        /// <param name="peakImport">Peak energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetPeakEnergyImport(int peakImport)
        {
            Guard.Argument(peakImport, nameof(peakImport)).Min(0);

            _outputPost.PeakEnergyImport = peakImport;
            return this;
        }

        /// <summary>
        /// Sets off peak energy import for the output.
        /// </summary>
        /// <param name="offpeakImport">Off peak energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetOffPeakEnergyImport(int offpeakImport)
        {
            Guard.Argument(offpeakImport, nameof(offpeakImport)).Min(0);

            _outputPost.OffPeakEnergyImport = offpeakImport;
            return this;
        }

        /// <summary>
        /// Sets shoulder energy import for the output.
        /// </summary>
        /// <param name="shoulderImport">Shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetShoulderEnergyImport(int shoulderImport)
        {
            Guard.Argument(shoulderImport, nameof(shoulderImport)).Min(0);

            _outputPost.ShoulderEnergyImport = shoulderImport;
            return this;
        }

        /// <summary>
        /// Sets high shoulder energy import for the output.
        /// </summary>
        /// <param name="highShoulderImport">High shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetHighShoulderEnergyImport(int highShoulderImport)
        {
            Guard.Argument(highShoulderImport, nameof(highShoulderImport)).Min(0);

            _outputPost.HighShoulderEnergyImport = highShoulderImport;
            return this;
        }

        /// <summary>
        /// Sets the total energy consumption for the output.
        /// </summary>
        /// <param name="consumption">Energy consumption</param>
        /// <returns>The builder.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        public OutputPostBuilder<TResultType> SetConsumption(int consumption)
        {
            Guard.Argument(consumption, nameof(consumption)).Min(0);

            if (typeof(TResultType) == typeof(IBatchOutputPost))
            {
                throw new InvalidOperationException("Cannot set consumption for batch output");
            }

            _outputPost.Consumption = consumption;
            return this;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new output.
        /// </summary>
        public void Reset()
        {
            _outputPost = new OutputPost();
        }

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// </summary>
        /// <returns>The output <typeparamref name="TResultType"/>.</returns>
        public TResultType Build()
        {
            ValidateStatus();
            return _outputPost as TResultType;
        }

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The output <typeparamref name="TResultType"/>.</returns>
        public TResultType BuildAndReset()
        {
            TResultType result = Build();
            Reset();
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        private void ValidateStatus()
        {
            if (_outputPost.OutputDate == DateTime.MinValue)
            {
                throw new InvalidOperationException("Output has no date");
            }

            if (_outputPost.PeakTime.HasValue && !_outputPost.OutputDate.Date.Equals(_outputPost.PeakTime.Value.Date))
            {
                throw new InvalidOperationException($"Peaktime registered on different date ({_outputPost.PeakTime.Value.ToShortDateString()}) than output itself ({_outputPost.OutputDate.ToShortDateString()})");
            }
        }
    }
}
