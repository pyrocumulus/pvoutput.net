using System;
using Dawn;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Builders
{
    /// <summary>
    /// Builder that creates to post to PVOutput.
    /// </summary>
    public abstract class BaseOutputPostBuilder<TResultType, TBuilderType>
        where TResultType : class, IBaseOutputPost
        where TBuilderType : class
    {
        internal TResultType OutputPost { get; set; }

        /// <summary>
        /// Sets the date for the output for the output.
        /// </summary>
        /// <param name="date">The date to set.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetDate(DateTime date)
        {
            Guard.Argument(date, nameof(date)).IsNoFutureDate().NoTimeComponent();

            OutputPost.OutputDate = date;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the total energy generated for the output.
        /// </summary>
        /// <param name="energyGenerated">Total energy generated.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetEnergyGenerated(int energyGenerated)
        {
            Guard.Argument(energyGenerated, nameof(energyGenerated)).Min(0);

            OutputPost.EnergyGenerated = energyGenerated;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the total energy exported for the output.
        /// </summary>
        /// <param name="energyExported">Total energy exported.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetEnergyExported(int energyExported)
        {
            Guard.Argument(energyExported, nameof(energyExported)).Min(0);

            OutputPost.EnergyExported = energyExported;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the time at which peak power was recorded.
        /// </summary>
        /// <param name="hours">Hour-component of the peak time.</param>
        /// <param name="minutes">Minute-component of the peak time.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetPeakTime(int hours, int minutes)
        {
            OutputPost.PeakTime = new TimeSpan(hours, minutes, 0);
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the time at which peak power was recorded.
        /// </summary>
        /// <param name="peakTime">The peak time.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetPeakTime(TimeSpan peakTime)
        {
            OutputPost.PeakTime = peakTime;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the peak power for the output.
        /// </summary>
        /// <param name="peakPower">Peak power.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetPeakPower(int peakPower)
        {
            Guard.Argument(peakPower, nameof(peakPower)).Min(0);

            OutputPost.PeakPower = peakPower;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the condition for the output.
        /// </summary>
        /// <param name="condition">The weather conditions on the output day.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetCondition(WeatherCondition condition)
        {
            OutputPost.Condition = condition;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets the minimum and maximum temperatures for the output.
        /// </summary>
        /// <param name="minimumTemperature">Minimum recorded temperature.</param>
        /// <param name="maximumTemperature">Maximum recorded temperature.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetTemperatures(decimal? minimumTemperature, decimal? maximumTemperature)
        {
            Guard.NotAllNull(Guard.Argument(minimumTemperature, nameof(minimumTemperature)), Guard.Argument(maximumTemperature, nameof(maximumTemperature)));

            if (minimumTemperature.HasValue && maximumTemperature.HasValue)
            {
                Guard.Argument(maximumTemperature.Value, nameof(maximumTemperature)).GreaterThan(minimumTemperature.Value);
            }

            OutputPost.MinimumTemperature = minimumTemperature;
            OutputPost.MaximumTemperature = maximumTemperature;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets comments for the output.
        /// </summary>
        /// <param name="comments">Comments.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetComments(string comments)
        {
            Guard.Argument(comments, nameof(comments)).NotEmpty().NotNull();

            OutputPost.Comments = comments;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets peak energy import for the output.
        /// </summary>
        /// <param name="peakImport">Peak energy import.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetPeakEnergyImport(int peakImport)
        {
            Guard.Argument(peakImport, nameof(peakImport)).Min(0);

            OutputPost.PeakEnergyImport = peakImport;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets off peak energy import for the output.
        /// </summary>
        /// <param name="offpeakImport">Off peak energy import.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetOffPeakEnergyImport(int offpeakImport)
        {
            Guard.Argument(offpeakImport, nameof(offpeakImport)).Min(0);

            OutputPost.OffPeakEnergyImport = offpeakImport;
            return this as TBuilderType;
        }

        /// <summary>
        /// Sets shoulder energy import for the output.
        /// </summary>
        /// <param name="shoulderImport">Shoulder energy import.</param>
        /// <returns>The builder.</returns>
        public TBuilderType SetShoulderEnergyImport(int shoulderImport)
        {
            Guard.Argument(shoulderImport, nameof(shoulderImport)).Min(0);

            OutputPost.ShoulderEnergyImport = shoulderImport;
            return this as TBuilderType;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new output.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// </summary>
        /// <returns>The output <typeparamref name="TResultType"/>.</returns>
        public TResultType Build()
        {
            ValidateStatus();
            return OutputPost;
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

        /// <summary>
        /// Validates the status of the output.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        protected internal virtual void ValidateStatus()
        {
            if (OutputPost.OutputDate == DateTime.MinValue)
            {
                throw new InvalidOperationException("Output has no date");
            }
        }
    }
}
