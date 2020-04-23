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

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public class ExtendedDataDefinitionBuilderTests
    {
        [Test]
        public void Builder_WithLabel_SetsLabel()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("New label");
            Assert.AreEqual("New label", builder._definition.Label);
        }

        [Test]
        public void Builder_WithUnit_SetsUnit()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetUnit("Wh");
            Assert.AreEqual("Wh", builder._definition.Unit);
        }

        [Test]
        public void Builder_WithAxis_SetsAxis()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetAxis(2);
            Assert.AreEqual(2, builder._definition.Axis);
        }

        [Test]
        public void Builder_WithDisplayType_SetsDisplayType()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetDisplayType(ExtendedDataDisplayType.Area);
            Assert.AreEqual(ExtendedDataDisplayType.Area, builder._definition.DisplayType);
        }

        [Test]
        public void Builder_WithColour_SetsColour()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetColour("123abc");
            Assert.AreEqual("123abc", builder._definition.Colour);
        }

        [Test]
        public void Builder_WithNonHexadecimalColour_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var builder = new ExtendedDataDefinitionBuilder().SetColour("test12");
            });
        }

        [Test]
        public void Builder_AfterReset_HasNoStateLeft()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("Test").SetUnit("W");
            IExtendedDataDefinition status = builder.Build();

            builder.Reset();

            Assert.AreNotSame(status, builder._definition);
        }


        [Test]
        public void Builder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("Test").SetUnit("W");
            IExtendedDataDefinition status = builder.BuildAndReset();

            Assert.AreNotSame(status, builder._definition);
        }
    }
}
