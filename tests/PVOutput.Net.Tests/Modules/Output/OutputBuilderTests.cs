using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Builders;
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
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today);

            Assert.AreEqual(DateTime.Today, builder._outputPost.Date);
        }

        [Test]
        public void OutputPostBuilder_WithGeneration_SetsGeneration()
        { 
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetGenerated(12121);

            Assert.AreEqual(12121, builder._outputPost.EnergyGenerated);
        }


        [Test]
        public void OutputPostBuilder_WithExported_SetsExported()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetExported(12121);

            Assert.AreEqual(12121, builder._outputPost.EnergyExported);
        }

        [Test]
        public void OutputPostBuilder_WithPeakTime_SetsPeakTime()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(new DateTime(2020, 1, 1))
                .SetPeakTime(new DateTime(2020, 1, 1, 10, 10, 0));

            Assert.AreEqual(new DateTime(2020, 1, 1, 10, 10, 0), builder._outputPost.PeakTime.Value);
        }


        [Test]
        public void OutputPostBuilder_WithPeakTimeOnOtherDate_CannotBuild()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetPeakTime(new DateTime(2020, 1, 1, 10, 10, 0));

            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }

        [Test]
        public void OutputPostBuilder_WithPeakPower_SetsPeakPower()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetPeakPower(13131);

            Assert.AreEqual(13131, builder._outputPost.PeakPower);
        }

        [Test]
        public void OutputPostBuilder_WithCondition_SetsCondition()
        {
            const string testCondition = "Test";
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetCondition(testCondition);

            Assert.AreEqual(testCondition, builder._outputPost.Condition);
        }

        [Test]
        [TestCase(new object[] { 10, 20 })]
        [TestCase(new object[] { null, 30 })]
        [TestCase(new object[] { 15, null })]
        public void OutputPostBuilder_WithTemperatures_SetsCorrectTemperature(decimal? minimum, decimal? maximum)
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetTemperatures(minimum, maximum);

            Assert.AreEqual(minimum, builder._outputPost.MinimumTemperature);
            Assert.AreEqual(maximum, builder._outputPost.MaximumTemperature);
        }

        [Test]
        public void OutputPostBuilder_WithComment_SetsComment()
        {
            const string testComment = "This is a comment";
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetComments(testComment);

            Assert.AreEqual(testComment, builder._outputPost.Comments);
        }

        [Test]
        public void OutputPostBuilder_WithPeakEnergyImport_SetsPeakEnergyImport()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetPeakEnergyImport(13131);

            Assert.AreEqual(13131, builder._outputPost.PeakEnergyImport);
        }

        [Test]
        public void OutputPostBuilder_WithOffPeakImport_SetsOffPeakEnergyImport()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetOffPeakEnergyImport(13131);

            Assert.AreEqual(13131, builder._outputPost.OffPeakEnergyImport);
        }

        [Test]
        public void OutputPostBuilder_WithShoulderEnergyImport_SetShoulderEnergyImport()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetShoulderEnergyImport(13131);

            Assert.AreEqual(13131, builder._outputPost.ShoulderEnergyImport);
        }

        [Test]
        public void OutputPostBuilder_WithHighShoulderEnergyImport_SetHighShoulderEnergyImport()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetHighShoulderEnergyImport(13131);

            Assert.AreEqual(13131, builder._outputPost.HighShoulderEnergyImport);
        }

        [Test]
        public void OutputPostBuilder_WithConsumption_SetConsumption()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today)
                .SetConsumption(25000);

            Assert.AreEqual(25000, builder._outputPost.Consumption);
        }

        [Test]
        public void OutputPostBuilder_WithoutDate_CannotBuild()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetGenerated(10000);
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }

        [Test]
        public void OutputPostBuilder_AfterReset_HasNoStateLeft()
        {
            var builder = new OutputPostBuilder<IOutputPost>().SetDate(DateTime.Today);
            IOutputPost output = builder.Build();

            builder.Reset();

            Assert.AreNotSame(output, builder._outputPost);
        }

        [Test]
        public void BatchOutputPostBuilder_WithConsumption_ThrowsException()
        {
            var builder = new OutputPostBuilder<IBatchOutputPost>().SetDate(DateTime.Today);
            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.SetConsumption(1200);
            });
        }
    }
}
