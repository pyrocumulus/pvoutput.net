using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    public sealed class OutputPostBuilder<TResultType> where TResultType : class, IBatchOutputPost
    {
        internal OutputPost _outputPost;

        public OutputPostBuilder()
        {
            _outputPost = new OutputPost();
        }

        public OutputPostBuilder<TResultType> SetDate(DateTime date)
        {
            _outputPost.OutputDate = date;
            return this;
        }

        public OutputPostBuilder<TResultType> SetGenerated(int energyGenerated)
        {
            _outputPost.EnergyGenerated = energyGenerated;
            return this;
        }

        public OutputPostBuilder<TResultType> SetExported(int energyExported)
        {
            _outputPost.EnergyExported = energyExported;
            return this;
        }

        public OutputPostBuilder<TResultType> SetPeakTime(DateTime peakTime)
        {
            _outputPost.PeakTime = peakTime;
            return this;
        }

        public OutputPostBuilder<TResultType> SetPeakPower(int peakPower)
        {
            _outputPost.PeakPower = peakPower;
            return this;
        }

        // TODO: Verify if any value is accepted or just a fixed list
        public OutputPostBuilder<TResultType> SetCondition(string condition)
        {
            _outputPost.Condition = condition;
            return this;
        }

        public OutputPostBuilder<TResultType> SetTemperatures(decimal? minimumTemperature, decimal? maximumTemperature)
        {
            _outputPost.MinimumTemperature = minimumTemperature;
            _outputPost.MaximumTemperature = maximumTemperature;
            return this;
        }

        public OutputPostBuilder<TResultType> SetComments(string comments)
        {
            _outputPost.Comments = comments;
            return this;
        }

        public OutputPostBuilder<TResultType> SetPeakEnergyImport(int peakImport)
        {
            _outputPost.PeakEnergyImport = peakImport;
            return this;
        }

        public OutputPostBuilder<TResultType> SetOffPeakEnergyImport(int offpeakImport)
        {
            _outputPost.OffPeakEnergyImport = offpeakImport;
            return this;
        }

        public OutputPostBuilder<TResultType> SetShoulderEnergyImport(int shoulderImport)
        {
            _outputPost.ShoulderEnergyImport = shoulderImport;
            return this;
        }

        public OutputPostBuilder<TResultType> SetHighShoulderEnergyImport(int highShoulderImport)
        {
            _outputPost.HighShoulderEnergyImport = highShoulderImport;
            return this;
        }

        public OutputPostBuilder<TResultType> SetConsumption(int consumption)
        {
            if (typeof(TResultType) == typeof(IBatchOutputPost))
            {
                throw new InvalidOperationException("Cannot set consumption on batch output");
            }

            _outputPost.Consumption = consumption;
            return this;
        }

        public void Reset()
        {
            _outputPost = new OutputPost();
        }

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
