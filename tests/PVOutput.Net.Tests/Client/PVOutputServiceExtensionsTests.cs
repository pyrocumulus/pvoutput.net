using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using PVOutput.Net.DependencyInjection;

namespace PVOutput.Net.Tests.Client
{
    [TestFixture]
    public class PVOutputServiceExtensionsTests
    {
        [Test]
        public void AddPVOutputClient_Throws_WithNoOptionsAction()
        {
            var services = Substitute.For<IServiceCollection>();

            Assert.Throws<ArgumentNullException>(() => {
                services.AddPVOutputClient(null);
            });
        }

        [Test]
        public void AddPVOutputClient_WithOptionsAction_ExcutesAction()
        {
            bool triggered = false;
            var services = Substitute.For<IServiceCollection>();

            services.AddPVOutputClient(o => { triggered = true; });

            Assert.IsTrue(triggered);
        }
    }
}
