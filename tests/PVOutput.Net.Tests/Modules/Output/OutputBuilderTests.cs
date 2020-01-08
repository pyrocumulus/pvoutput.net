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
