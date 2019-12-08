using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Builders
{
    public class OutputPostBuilder
    {
        private readonly OutputPost _outputPost;

        public OutputPostBuilder()
        {
            _outputPost = new OutputPost();
        }

        public OutputPostBuilder SetDate(DateTime date)
        {
            _outputPost.Date = date;
            return this;
        }

        public OutputPostBuilder SetGenerated(int energyGenerated)
        {
            _outputPost.EnergyGenerated = energyGenerated;
            return this;
        }

        public OutputPostBuilder SetExported(int? energyExported)
        {
            _outputPost.EnergyExported = energyExported;
            return this;
        }

        public OutputPostBuilder SetPeakTime(DateTime peakTime)
        {
            _outputPost.PeakTime = peakTime;
            return this;
        }

        // TODO: Verify if any value is accepted or just a fixed list
        public OutputPostBuilder SetCondition(string condition)
        {
            _outputPost.Condition = condition;
            return this;
        }

        public OutputPostBuilder SetTemperatures(decimal? minimumTemperature, decimal? maximumTemperature)
        {
            _outputPost.MinimumTemperature = minimumTemperature;
            _outputPost.MaximumTemperature = maximumTemperature;
            return this;
        }

        public OutputPostBuilder SetComments(string comments)
        {
            _outputPost.Comments = comments;
            return this;
        }

        public OutputPostBuilder SetPeakImport(int peakImport)
        {
            _outputPost.PeakEnergyImport = peakImport;
            return this;
        }

        public OutputPostBuilder SetOffPeakEnergyImport(int offpeakImport)
        {
            _outputPost.OffPeakEnergyImport = offpeakImport;
            return this;
        }

        public OutputPostBuilder SetShoulderImport(int shoulderImport)
        {
            _outputPost.ShoulderEnergyImport = shoulderImport;
            return this;
        }

        public OutputPostBuilder SetHighShoulderImport(int highShoulderImport)
        {
            _outputPost.HighShoulderEnergyImport = highShoulderImport;
            return this;
        }

        public OutputPostBuilder SetConsumption(int consumption)
        {
            _outputPost.Consumption = consumption;
            return this;
        }

        public IOutputPost Build()
        {
            return _outputPost;
        }
    }
}
