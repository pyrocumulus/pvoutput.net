using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using PVOutput.Net.Enums;
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
            _outputPost.PeakPower = peakPower;
            return this;
        }

        /// <summary>
        /// Sets the condition for the output.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The builder.</returns>
        public OutputPostBuilder<TResultType> SetCondition(string condition)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        public TResultType Build()
        {
            if (_outputPost.OutputDate == DateTime.MinValue)
            {
                throw new InvalidOperationException("Output has no date");
            }

            if (_outputPost.PeakTime.HasValue && !_outputPost.OutputDate.Date.Equals(_outputPost.PeakTime.Value.Date))
            {
                throw new InvalidOperationException($"Peaktime registered on different date ({_outputPost.PeakTime.Value.ToShortDateString()}) than output itself ({_outputPost.OutputDate.ToShortDateString()})");            }

            return _outputPost as TResultType;
        }
    }
}
