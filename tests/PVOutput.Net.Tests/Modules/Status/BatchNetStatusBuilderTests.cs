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
    public class BatchNetStatusBuilderTests
    {
        [Test]
        public void BatchNetStatusBuilder_WithTimeStamp_SetsTimeStamp()
        { 
            var timeStamp = DateTime.Now;
            var builder = new StatusPostBuilder<IStatusPost>().SetTimeStamp(timeStamp);

            Assert.That(builder._statusPost.Timestamp, Is.EqualTo(timeStamp));
        }

        [Test]
        public void BatchNetStatusBuilder_WithFutureTimeStamp_Throws()
        {
            var timeStamp = DateTime.Now.AddDays(1);
            var builder = new StatusPostBuilder<IStatusPost>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.SetTimeStamp(timeStamp);
            });
        }

        [Test]
        [TestCase(100)]
        [TestCase(0)]
        public void BatchNetStatusBuilder_WithPowerExported_SetsPowerExported(int powerExported)
        {
            var builder = new BatchNetStatusPostBuilder().SetPowerExported(powerExported);

            Assert.That(builder._statusPost.PowerExported, Is.EqualTo(powerExported));
        }

        [Test]
        [TestCase(100)]
        [TestCase(0)]
        public void BatchNetStatusBuilder_WithPowerImported_SetsPowerImported(int powerImported)
        {
            var builder = new BatchNetStatusPostBuilder().SetPowerImported(powerImported);

            Assert.That(builder._statusPost.PowerImported, Is.EqualTo(powerImported));
        }

        [Test]
        public void BatchNetStatusBuilder_AfterReset_HasNoStateLeft()
        {
            var builder = new BatchNetStatusPostBuilder().SetPowerExported(1000).SetTimeStamp(DateTime.Now);
            IBatchNetStatusPost status = builder.Build();

            builder.Reset();

            Assert.That(builder._statusPost, Is.Not.SameAs(status));
        }


        [Test]
        public void BatchNetStatusBuilder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new BatchNetStatusPostBuilder().SetPowerImported(1000).SetTimeStamp(DateTime.Now);
            IBatchNetStatusPost status = builder.BuildAndReset();

            Assert.That(builder._statusPost, Is.Not.SameAs(status));
        }

        [Test]
        public void BatchNetStatusBuilder_WithoutPowerOrConsumption_CannotBuild()
        {
            var builder = new BatchNetStatusPostBuilder().SetTimeStamp(DateTime.Now);

            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.Build();
            });
        }
    }
}
