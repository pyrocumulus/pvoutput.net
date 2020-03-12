using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
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

            Assert.AreEqual(timeStamp, builder._statusPost.Timestamp);
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
        [TestCase(new object[] { 10, 20 })]
        [TestCase(new object[] { null, 30 })]
        [TestCase(new object[] { 15, null })]
        public void StatusPostBuilder_WithGeneration_SetsGeneration(int? energyGeneration, int? powerGeneration)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetGeneration(energyGeneration, powerGeneration);

            Assert.AreEqual(energyGeneration, builder._statusPost.EnergyGeneration);
            Assert.AreEqual(powerGeneration, builder._statusPost.PowerGeneration);
        }

        [Test]
        [TestCase(new object[] { 10, 20 })]
        [TestCase(new object[] { null, 30 })]
        [TestCase(new object[] { 15, null })]
        public void StatusPostBuilder_WithConsumption_SetsConsumption(int? energyConsumption, int? powerConsumption)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetConsumption(energyConsumption, powerConsumption);

            Assert.AreEqual(energyConsumption, builder._statusPost.EnergyConsumption);
            Assert.AreEqual(powerConsumption, builder._statusPost.PowerConsumption);
        }

        [Test]
        public void StatusPostBuilder_WithTemperature_SetsTemperature()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetTemperature(15.1m);

            Assert.AreEqual(15.1m, builder._statusPost.Temperature);
        }

        [Test]
        public void StatusPostBuilder_WithVoltage_SetsVoltage()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetVoltage(231.2m);

            Assert.AreEqual(231.2m, builder._statusPost.Voltage);
        }

        [Test]
        public void StatusPostBuilder_WithCumulativeType_SetsCumulativeType()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetCumulativeType(CumulativeStatusType.LifetimeConsumption);

            Assert.AreEqual(CumulativeStatusType.LifetimeConsumption, builder._statusPost.Cumulative);
        }

        [Test]
        public void StatusPostBuilder_WithNetValue_SetsNetValue()
        {
            var builder = new StatusPostBuilder<IStatusPost>().IsNetValue(true);

            Assert.AreEqual(true, builder._statusPost.Net);
        }

        [Test]
        [TestCase(new object[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new object[] { 1, null, null, null, null, null })]
        [TestCase(new object[] { null, 2, null, null, null, null })]
        [TestCase(new object[] { null, null, 3, null, null, null })]
        [TestCase(new object[] { null, null, null, 4, null, null })]
        [TestCase(new object[] { null, null, null, null, 5, null })]
        [TestCase(new object[] { null, null, null, null, null, 6 })]
        public void StatusPostBuilder_WithExtendedValues_SetsExtendedValues(decimal? value1, decimal? value2, decimal? value3, decimal? value4, decimal? value5, decimal? value6)
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetExtendedValues(value1, value2, value3, value4, value5, value6);

            Assert.AreEqual(value1, builder._statusPost.ExtendedValue1);
            Assert.AreEqual(value2, builder._statusPost.ExtendedValue2);
            Assert.AreEqual(value3, builder._statusPost.ExtendedValue3);
            Assert.AreEqual(value4, builder._statusPost.ExtendedValue4);
            Assert.AreEqual(value5, builder._statusPost.ExtendedValue5);
            Assert.AreEqual(value6, builder._statusPost.ExtendedValue6);
        }


        [Test]
        public void StatusPostBuilder_WithTextMessage_SetsTextMessage()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetTextMessage("This is a test");

            Assert.AreEqual("This is a test", builder._statusPost.TextMessage);
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

            Assert.AreNotSame(status, builder._statusPost);
        }


        [Test]
        public void StatusPostBuilder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new StatusPostBuilder<IStatusPost>().SetGeneration(1000, null).SetTimeStamp(DateTime.Now);
            IStatusPost status = builder.BuildAndReset();

            Assert.AreNotSame(status, builder._statusPost);
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
