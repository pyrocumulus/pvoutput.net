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

namespace PVOutput.Net.Tests.Modules.Output
{
    [TestFixture]
    public class OutputBuilderTests
    {
        [Test]
        public void OutputPostBuilder_WithDate_SetsDate()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);

            Assert.That(builder.OutputPost.OutputDate, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void OutputPostBuilder_WithTimeComponent_Throws()
        {
            Assert.Throws<ArgumentException>(() => 
            {
                _ = new OutputPostBuilder().SetDate(DateTime.Today.AddMinutes(10));
            });
        }

        [Test]
        public void OutputPostBuilder_WithGeneration_SetsGeneration()
        { 
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetEnergyGenerated(12121);

            Assert.That(builder.OutputPost.EnergyGenerated, Is.EqualTo(12121));
        }

        [Test]
        public void OutputPostBuilder_WithNegativeGeneration_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetEnergyGenerated(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithExported_SetsExported()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetEnergyExported(12121);

            Assert.That(builder.OutputPost.EnergyExported, Is.EqualTo(12121));
        }


        [Test]
        public void OutputPostBuilder_WithNegativeExported_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetEnergyExported(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithPeakTime_SetsPeakTime()
        {
            var builder = new OutputPostBuilder().SetDate(new DateTime(2020, 1, 1))
                .SetPeakTime(12, 22);

            Assert.That(builder.OutputPost.PeakTime.Value, Is.EqualTo(new TimeSpan(12, 22, 0)));
        }

        [Test]
        public void OutputPostBuilder_WithPeakTimeSpan_SetsPeakTime()
        {
            var builder = new OutputPostBuilder().SetDate(new DateTime(2020, 1, 1))
                .SetPeakTime(new TimeSpan(10, 10, 0));

            Assert.That(builder.OutputPost.PeakTime.Value, Is.EqualTo(new TimeSpan(10, 10, 0)));
        }

        [Test]
        public void OutputPostBuilder_WithPeakPower_SetsPeakPower()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetPeakPower(13131);

            Assert.That(builder.OutputPost.PeakPower, Is.EqualTo(13131));
        }

        [Test]
        public void OutputPostBuilder_WithNegativePeakPower_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetPeakPower(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithCondition_SetsCondition()
        {
            const WeatherCondition testCondition = WeatherCondition.PartlyCloudy;
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetCondition(testCondition);

            Assert.That(builder.OutputPost.Condition, Is.EqualTo(testCondition));
        }

        [Test]
        [TestCase(10, 20)]
        [TestCase(null, 30)]
        [TestCase(15, null)]
        public void OutputPostBuilder_WithTemperatures_SetsCorrectTemperature(decimal? minimum, decimal? maximum)
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetTemperatures(minimum, maximum);

            Assert.That(builder.OutputPost.MinimumTemperature, Is.EqualTo(minimum));
            Assert.That(builder.OutputPost.MaximumTemperature, Is.EqualTo(maximum));
        }

        [Test]
        public void OutputPostBuilder_WithReversedTemperatures_Throws()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.SetTemperatures(15, 10);
            });
        }

        [Test]
        public void OutputPostBuilder_WithBothNullTemperatures_Throws()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);

            Assert.Throws<ArgumentNullException>(() =>
            {
                builder.SetTemperatures(null, null);
            });
        }

        [Test]
        public void OutputPostBuilder_WithComment_SetsComment()
        {
            const string testComment = "This is a comment";
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetComments(testComment);

            Assert.That(builder.OutputPost.Comments, Is.EqualTo(testComment));
        }

        [Test]
        public void OutputPostBuilder_WithEmptyComment_Throws()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);

            Assert.Throws<ArgumentException>(() =>
            {
                builder.SetComments("");
            });
        }

        [Test]
        public void OutputPostBuilder_WithNullComment_Throws()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);

            Assert.Throws<ArgumentNullException>(() =>
            {
                builder.SetComments(null);
            });
        }

        [Test]
        public void OutputPostBuilder_WithPeakEnergyImport_SetsPeakEnergyImport()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetPeakEnergyImport(13131);

            Assert.That(builder.OutputPost.PeakEnergyImport, Is.EqualTo(13131));
        }

        [Test]
        public void OutputPostBuilder_WithNegativePeakEnergyImport_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetPeakEnergyImport(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithOffPeakImport_SetsOffPeakEnergyImport()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetOffPeakEnergyImport(13131);

            Assert.That(builder.OutputPost.OffPeakEnergyImport, Is.EqualTo(13131));
        }

        [Test]
        public void OutputPostBuilder_WithNegativeOffPeakEnergyImport_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetOffPeakEnergyImport(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithShoulderEnergyImport_SetShoulderEnergyImport()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetShoulderEnergyImport(13131);

            Assert.That(builder.OutputPost.ShoulderEnergyImport, Is.EqualTo(13131));
        }

        [Test]
        public void OutputPostBuilder_WithNegativeShoulderEnergyImport_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetShoulderEnergyImport(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithHighShoulderEnergyImport_SetHighShoulderEnergyImport()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetHighShoulderEnergyImport(13131);

            Assert.That(builder.OutputPost.HighShoulderEnergyImport, Is.EqualTo(13131));
        }

        [Test]
        public void OutputPostBuilder_WithNegativeHighShoulderEnergyImport_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetHighShoulderEnergyImport(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithConsumption_SetConsumption()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today)
                .SetConsumption(25000);

            Assert.That(builder.OutputPost.Consumption, Is.EqualTo(25000));
        }

        [Test]
        public void OutputPostBuilder_WithNegativeConsumption_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new OutputPostBuilder().SetConsumption(-1);
            });
        }

        [Test]
        public void OutputPostBuilder_WithoutDate_CannotBuild()
        {
            var builder = new OutputPostBuilder().SetEnergyGenerated(10000);
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }

        [Test]
        public void OutputPostBuilder_AfterReset_HasNoStateLeft()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);
            IOutputPost output = builder.Build();

            builder.Reset();

            Assert.That(builder.OutputPost, Is.Not.SameAs(output));
        }

        [Test]
        public void OutputPostBuilder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new OutputPostBuilder().SetDate(DateTime.Today);
            IOutputPost output = builder.BuildAndReset();

            Assert.That(builder.OutputPost, Is.Not.SameAs(output));
        }

        [Test]
        public void BatchOutputPostBuilder_WithEnergyUsed_SetsUsed()
        {
            var builder = new BatchOutputPostBuilder().SetDate(DateTime.Today)
                .SetEnergyUsed(31132);

            Assert.That(builder.OutputPost.EnergyUsed, Is.EqualTo(31132));
        }

        [Test]
        public void BatchOutputPostBuilder_AfterReset_HasNoStateLeft()
        {
            var builder = new BatchOutputPostBuilder().SetDate(DateTime.Today);
            IBatchOutputPost output = builder.OutputPost;

            builder.Reset();

            Assert.That(builder.OutputPost, Is.Not.SameAs(output));
        }

        [Test]
        public void BatchOutputPostBuilder_WithoutEnergyGeneratedAndEnergyUsed_CannotBuild()
        {
            var builder = new BatchOutputPostBuilder().SetDate(DateTime.Today);
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }
    }
}
