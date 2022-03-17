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

            Assert.Throws<ArgumentException>(() =>
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
    }
}
