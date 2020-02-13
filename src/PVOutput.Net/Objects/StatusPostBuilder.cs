using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    public sealed class StatusPostBuilder<TResultType> where TResultType : class, IBatchStatusPost
    {
        private StatusPost _statusPost;

        public StatusPostBuilder()
        {
            _statusPost = new StatusPost();
        }

        public StatusPostBuilder<TResultType> SetDate(DateTime date)
        {
            _statusPost.Timestamp = date;
            return this;
        }

        public StatusPostBuilder<TResultType> SetGeneration(int? energyGeneration, int? powerGeneration)
        {
            _statusPost.EnergyGeneration = energyGeneration;
            _statusPost.PowerGeneration = powerGeneration;
            return this;
        }

        public StatusPostBuilder<TResultType> SetConsumption(int? energyConsumption, int? powerConsumption)
        {
            _statusPost.EnergyConsumption = energyConsumption;
            _statusPost.PowerConsumption = powerConsumption;
            return this;
        }

        public StatusPostBuilder<TResultType> SetTemperature(decimal temperature)
        {
            _statusPost.Temperature = temperature;
            return this;
        }

        public StatusPostBuilder<TResultType> SetVoltage(decimal voltage)
        {
            _statusPost.Voltage = voltage;
            return this;
        }

        public StatusPostBuilder<TResultType> SetCumulativeType(CumulativeStatusType type)
        {
            _statusPost.Cumulative = type;
            return this;
        }

        public StatusPostBuilder<TResultType> IsNetValue(bool net = true)
        {
            _statusPost.Net = net;
            return this;
        }

        public StatusPostBuilder<TResultType> SetExtendedValues(decimal? value1, decimal? value2, decimal? value3, decimal? value4, decimal? value5, decimal? value6)
        {
            _statusPost.ExtendedValue1 = value1;
            _statusPost.ExtendedValue2 = value2;
            _statusPost.ExtendedValue3 = value3;
            _statusPost.ExtendedValue4 = value4;
            _statusPost.ExtendedValue5 = value5;
            _statusPost.ExtendedValue6 = value6;
            return this;
        }

        public StatusPostBuilder<TResultType> SetTextMessage(string textMessage)
        {
            _statusPost.TextMessage = textMessage;
            return this;
        }

        public void Reset()
        {
            _statusPost = new StatusPost();
        }

        public TResultType Build()
        {
            ValidateStatus();
            
            var result = _statusPost as TResultType;
            Reset();
            return result;
        }

        private void ValidateStatus()
        {
            if (_statusPost.EnergyGeneration == null 
                && _statusPost.PowerGeneration == null
                && _statusPost.EnergyConsumption == null 
                && _statusPost.PowerConsumption == null)
            {
                throw new InvalidOperationException("Status has no power or consumption values");
            }
        }
    }
}
