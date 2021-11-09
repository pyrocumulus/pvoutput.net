using System;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder that creates to post to PVOutput.
    /// </summary>
    public sealed class OutputPostBuilder
    {
        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public OutputPostBuilder()
        {
            Reset();
        }

        internal OutputPost OutputPost { get; set; }

        /// <summary>
        /// Sets the date for the output for the output.
        /// </summary>
        /// <param name="date">The date to set.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetDate(DateTime date)
        {
            Guard.Argument(date, nameof(date)).IsNoFutureDate().NoTimeComponent();

            OutputPost.OutputDate = date;
            return this;
        }

        /// <summary>
        /// Sets the total energy generated for the output.
        /// </summary>
        /// <param name="energyGenerated">Total energy generated.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetEnergyGenerated(int energyGenerated)
        {
            Guard.Argument(energyGenerated, nameof(energyGenerated)).Min(0);

            OutputPost.EnergyGenerated = energyGenerated;
            return this;
        }

        /// <summary>
        /// Sets the total energy exported for the output.
        /// </summary>
        /// <param name="energyExported">Total energy exported.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetEnergyExported(int energyExported)
        {
            Guard.Argument(energyExported, nameof(energyExported)).Min(0);

            OutputPost.EnergyExported = energyExported;
            return this;
        }

        /// <summary>
        /// Sets the time at which peak power was recorded.
        /// </summary>
        /// <param name="hours">Hour-component of the peak time.</param>
        /// <param name="minutes">Minute-component of the peak time.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetPeakTime(int hours, int minutes)
        {
            OutputPost.PeakTime = new TimeSpan(hours, minutes, 0);
            return this;
        }

        /// <summary>
        /// Sets the time at which peak power was recorded.
        /// </summary>
        /// <param name="peakTime">The peak time.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetPeakTime(TimeSpan peakTime)
        {
            OutputPost.PeakTime = peakTime;
            return this;
        }

        /// <summary>
        /// Sets the peak power for the output.
        /// </summary>
        /// <param name="peakPower">Peak power.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetPeakPower(int peakPower)
        {
            Guard.Argument(peakPower, nameof(peakPower)).Min(0);

            OutputPost.PeakPower = peakPower;
            return this;
        }

        /// <summary>
        /// Sets the condition for the output.
        /// </summary>
        /// <param name="condition">The weather conditions on the output day.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetCondition(WeatherCondition condition)
        {
            OutputPost.Condition = condition;
            return this;
        }

        /// <summary>
        /// Sets the minimum and maximum temperatures for the output.
        /// </summary>
        /// <param name="minimumTemperature">Minimum recorded temperature.</param>
        /// <param name="maximumTemperature">Maximum recorded temperature.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetTemperatures(decimal? minimumTemperature, decimal? maximumTemperature)
        {
            Guard.NotAllNull(Guard.Argument(minimumTemperature, nameof(minimumTemperature)), Guard.Argument(maximumTemperature, nameof(maximumTemperature)));

            if (minimumTemperature.HasValue && maximumTemperature.HasValue)
            {
                Guard.Argument(maximumTemperature.Value, nameof(maximumTemperature)).GreaterThan(minimumTemperature.Value);
            }

            OutputPost.MinimumTemperature = minimumTemperature;
            OutputPost.MaximumTemperature = maximumTemperature;
            return this;
        }

        /// <summary>
        /// Sets comments for the output.
        /// </summary>
        /// <param name="comments">Comments.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetComments(string comments)
        {
            Guard.Argument(comments, nameof(comments)).NotEmpty().NotNull();

            OutputPost.Comments = comments;
            return this;
        }

        /// <summary>
        /// Sets peak energy import for the output.
        /// </summary>
        /// <param name="peakImport">Peak energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetPeakEnergyImport(int peakImport)
        {
            Guard.Argument(peakImport, nameof(peakImport)).Min(0);

            OutputPost.PeakEnergyImport = peakImport;
            return this;
        }

        /// <summary>
        /// Sets off peak energy import for the output.
        /// </summary>
        /// <param name="offpeakImport">Off peak energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetOffPeakEnergyImport(int offpeakImport)
        {
            Guard.Argument(offpeakImport, nameof(offpeakImport)).Min(0);

            OutputPost.OffPeakEnergyImport = offpeakImport;
            return this;
        }

        /// <summary>
        /// Sets shoulder energy import for the output.
        /// </summary>
        /// <param name="shoulderImport">Shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetShoulderEnergyImport(int shoulderImport)
        {
            Guard.Argument(shoulderImport, nameof(shoulderImport)).Min(0);

            OutputPost.ShoulderEnergyImport = shoulderImport;
            return this;
        }

        /// <summary>
        /// Sets high shoulder energy import for the output.
        /// </summary>
        /// <param name="highShoulderImport">High shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetHighShoulderEnergyImport(int highShoulderImport)
        {
            Guard.Argument(highShoulderImport, nameof(highShoulderImport)).Min(0);

            OutputPost.HighShoulderEnergyImport = highShoulderImport;
            return this;
        }

        /// <summary>
        /// Sets the total energy consumption for the output.
        /// </summary>
        /// <param name="consumption">Energy consumption</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder SetConsumption(int consumption)
        {
            Guard.Argument(consumption, nameof(consumption)).Min(0);

            (OutputPost as OutputPost).Consumption = consumption;
            return this;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new output.
        /// </summary>
        public void Reset() => OutputPost = new OutputPost();

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// </summary>
        /// <returns>The output.</returns>
        public IOutputPost Build()
        {
            ValidateStatus();
            return OutputPost;
        }

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The output.</returns>
        public IOutputPost BuildAndReset()
        {
            IOutputPost result = Build();
            Reset();
            return result;
        }

        /// <summary>
        /// Validates the status of the output.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        private void ValidateStatus()
        {
            if (OutputPost.OutputDate == DateTime.MinValue)
            {
                throw new InvalidOperationException("Output has no date");
            }
        }
    }
}
