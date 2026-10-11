using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Builders;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Status
{
    [TestFixture]
    public class StatusBuilderTests
    {
        [Test]
        public void StatusPostBuilder_WithTimeStamp_SetsTimeStamp()
        { 
            var timeStamp = DateTime.Now;
            var builder = new StatusPostBuilder<IStatusPost>().SetTimeStamp(timeStamp);

            Assert.That(builder.StatusPost.Timestamp, Is.EqualTo(timeStamp));
        }

        [Test]
        public void StatusPostBuilder_WithFutureTimeStamp_Throws()
        {
            var timeStamp = DateTime.Now.AddDays(1);
            var builder = new StatusPostBuilder<IStatusPost>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.SetTimeStamp(timeStamp);
            });
        }

        [Test]
        [TestCase(10, 20)]
        [TestCase(null, 30)]
        [TestCase(15, null)]
        public void StatusPostBuilder_WithGeneration_SetsGeneration(int? energyGeneration, int? powerGeneration)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetGeneration(energyGeneration, powerGeneration);
            
            Assert.Multiple(() =>
            {
                Assert.That(builder.StatusPost.EnergyGeneration, Is.EqualTo(energyGeneration));
                Assert.That(builder.StatusPost.PowerGeneration, Is.EqualTo(powerGeneration));
            });
        }

        [Test]
        [TestCase(null, -1)]
        [TestCase(-1, null)]
        public void StatusPostBuilder_WithNegativeGeneration_Throws(int? energyGeneration, int? powerGeneration)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetGeneration(energyGeneration, powerGeneration);
            });
        }

        [Test]
        [TestCase(10, 20)]
        [TestCase(null, 30)]
        [TestCase(15, null)]
        public void StatusPostBuilder_WithConsumption_SetsConsumption(int? energyConsumption, int? powerConsumption)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetConsumption(energyConsumption, powerConsumption);
            
            Assert.Multiple(() =>
            {
                Assert.That(builder.StatusPost.EnergyConsumption, Is.EqualTo(energyConsumption));
                Assert.That(builder.StatusPost.PowerConsumption, Is.EqualTo(powerConsumption));
            });
        }

        [Test]
        [TestCase(null, -1)]
        [TestCase(-1, null)]
        public void StatusPostBuilder_WithNegativeConsumption_Throws(int? energyConsumption, int? powerConsumption)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetConsumption(energyConsumption, powerConsumption);
            });
        }

        [Test]
        public void StatusPostBuilder_WithTemperature_SetsTemperature()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetTemperature(15.1m);

            Assert.That(builder.StatusPost.Temperature, Is.EqualTo(15.1m));
        }

        [Test]
        public void StatusPostBuilder_WithVoltage_SetsVoltage()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetVoltage(231.2m);

            Assert.That(builder.StatusPost.Voltage, Is.EqualTo(231.2m));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void StatusPostBuilder_WithVoltageOutOfRange_Throws(decimal voltage)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetVoltage(voltage);
            });
        }

        [Test]
        public void StatusPostBuilder_WithCumulativeType_SetsCumulativeType()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetCumulativeType(CumulativeStatusType.LifetimeConsumption);

            Assert.That(builder.StatusPost.Cumulative, Is.EqualTo(CumulativeStatusType.LifetimeConsumption));
        }

        [Test]
        public void StatusPostBuilder_WithNetValue_SetsNetValue()
        {
            var builder = new StatusPostBuilder<IStatusPost>().IsNetValue(true);

            Assert.That(builder.StatusPost.Net, Is.EqualTo(true));
        }

        [Test]
        [TestCase(1, 2, 3, 4, 5, 6)]
        [TestCase(1, null, null, null, null, null)]
        [TestCase(null, 2, null, null, null, null)]
        [TestCase(null, null, 3, null, null, null)]
        [TestCase(null, null, null, 4, null, null)]
        [TestCase(null, null, null, null, 5, null)]
        [TestCase(null, null, null, null, null, 6)]
        public void StatusPostBuilder_WithExtendedValues_SetsExtendedValues(decimal? value1, decimal? value2, decimal? value3, decimal? value4, decimal? value5, decimal? value6)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetExtendedValues(value1, value2, value3, value4, value5, value6);
            
            Assert.Multiple(() =>
            {
                Assert.That(builder.StatusPost.ExtendedValue1, Is.EqualTo(value1));
                Assert.That(builder.StatusPost.ExtendedValue2, Is.EqualTo(value2));
                Assert.That(builder.StatusPost.ExtendedValue3, Is.EqualTo(value3));
                Assert.That(builder.StatusPost.ExtendedValue4, Is.EqualTo(value4));
                Assert.That(builder.StatusPost.ExtendedValue5, Is.EqualTo(value5));
                Assert.That(builder.StatusPost.ExtendedValue6, Is.EqualTo(value6));
            });
        }

        [Test]
        public void StatusPostBuilder_WithTextMessage_SetsTextMessage()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetTextMessage("This is a test");

            Assert.That(builder.StatusPost.TextMessage, Is.EqualTo("This is a test"));
        }

        [Test]
        public void StatusPostBuilder_WithTooLongTextMessage_Throws()
        {
            var builder = new StatusPostBuilder<IStatusPost>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.SetTextMessage("0123456789001234567890012345678901");
            });
        }

        [Test]
        public void StatusPostBuilder_AfterReset_HasNoStateLeft()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetGeneration(1000, null).SetTimeStamp(DateTime.Now);
            IStatusPost status = builder.Build();

            builder.Reset();

            Assert.That(builder.StatusPost, Is.Not.SameAs(status));
        }


        [Test]
        public void StatusPostBuilder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetGeneration(1000, null).SetTimeStamp(DateTime.Now);
            IStatusPost status = builder.BuildAndReset();

            Assert.That(builder.StatusPost, Is.Not.SameAs(status));
        }

        [Test]
        public void StatusPostBuilder_WithoutPowerOrConsumption_CannotBuild()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetTextMessage("Test");

            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }

        [Test]
        public void StatusPostBuilder_WithBattery_SetsBatteryValues()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetBattery(-1500.5m, 87.5m, 13500m, 250000m, 240000m, BatteryStatus.Enabled);

            Assert.Multiple(() =>
            {
                Assert.That(builder.StatusPost.BatteryPower, Is.EqualTo(-1500.5m));
                Assert.That(builder.StatusPost.BatteryStateOfCharge, Is.EqualTo(87.5m));
                Assert.That(builder.StatusPost.BatteryCapacity, Is.EqualTo(13500m));
                Assert.That(builder.StatusPost.BatteryLifetimeEnergyCharged, Is.EqualTo(250000m));
                Assert.That(builder.StatusPost.BatteryLifetimeEnergyDischarged, Is.EqualTo(240000m));
                Assert.That(builder.StatusPost.BatteryStatus, Is.EqualTo(BatteryStatus.Enabled));
            });
        }

        [Test]
        public void StatusPostBuilder_WithBatteryPowerOnly_LeavesOtherBatteryValuesEmpty()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetBattery(2000m);

            Assert.Multiple(() =>
            {
                Assert.That(builder.StatusPost.BatteryPower, Is.EqualTo(2000m));
                Assert.That(builder.StatusPost.BatteryStateOfCharge, Is.Null);
                Assert.That(builder.StatusPost.BatteryCapacity, Is.Null);
                Assert.That(builder.StatusPost.BatteryLifetimeEnergyCharged, Is.Null);
                Assert.That(builder.StatusPost.BatteryLifetimeEnergyDischarged, Is.Null);
                Assert.That(builder.StatusPost.BatteryStatus, Is.Null);
            });
        }

        [Test]
        [TestCase(-2500000.1)]
        [TestCase(2500000.1)]
        public void StatusPostBuilder_WithBatteryPowerOutOfRange_Throws(decimal power)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetBattery(power);
            });
        }

        [Test]
        [TestCase(-2500000)]
        [TestCase(2500000)]
        public void StatusPostBuilder_WithBatteryPowerAtLimits_SetsBatteryPower(decimal power)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetBattery(power);

            Assert.That(builder.StatusPost.BatteryPower, Is.EqualTo(power));
        }

        [Test]
        [TestCase(-0.1)]
        [TestCase(100.1)]
        public void StatusPostBuilder_WithStateOfChargeOutOfRange_Throws(decimal stateOfCharge)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetBattery(0m, stateOfCharge: stateOfCharge);
            });
        }

        [Test]
        public void StatusPostBuilder_WithNegativeBatteryCapacity_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetBattery(0m, capacity: -1m);
            });
        }

        [Test]
        public void StatusPostBuilder_WithNegativeLifetimeEnergyCharged_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetBattery(0m, lifetimeEnergyCharged: -1m);
            });
        }

        [Test]
        public void StatusPostBuilder_WithNegativeLifetimeEnergyDischarged_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new StatusPostBuilder<IStatusPost>().SetBattery(0m, lifetimeEnergyDischarged: -1m);
            });
        }
    }
}
