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
        public void Builder_WithIndex_SetsIndex()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetIndex(ExtendedDataIndex.v9);
            Assert.That(builder._definition.Index, Is.EqualTo(ExtendedDataIndex.v9));
        }

        [Test]
        public void Builder_WithLabel_SetsLabel()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("New label");
            Assert.That(builder._definition.Label, Is.EqualTo("New label"));
        }

        [Test]
        public void Builder_WithUnit_SetsUnit()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetUnit("Wh");
            Assert.That(builder._definition.Unit, Is.EqualTo("Wh"));
        }

        [Test]
        public void Builder_WithAxis_SetsAxis()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetAxis(2);
            Assert.That(builder._definition.Axis, Is.EqualTo(2));
        }

        [Test]
        public void Builder_WithDisplayType_SetsDisplayType()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetDisplayType(ExtendedDataDisplayType.Area);
            Assert.That(builder._definition.DisplayType, Is.EqualTo(ExtendedDataDisplayType.Area));
        }

        [Test]
        public void Builder_WithColour_SetsColour()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetColour("123aBC");
            Assert.That(builder._definition.Colour, Is.EqualTo("123aBC"));
        }

        [Test]
        public void Builder_WithNonHexadecimalColour_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var builder = new ExtendedDataDefinitionBuilder().SetColour("abcT12");
            });
        }

        public static IEnumerable HexadecimalValidationsTests
        {
            get
            {
                foreach (char c in "0123456789ABCDEF".ToList())
                {
                    yield return new TestCaseData(c).Returns(true);
                }
                foreach (char c in "GHIJKLMNOPQRSTUVWXYZ.,;\\[]#".ToList())
                {
                    yield return new TestCaseData(c).Returns(false);
                }
            }
        }               

        [TestCaseSource(typeof(ExtendedDataDefinitionBuilderTests), nameof(HexadecimalValidationsTests))]
        public bool Builder_HexadecimalValidations_WorksCorrect(char colour)
        {
            return ExtendedDataDefinitionBuilder.IsHexadecimalCharacter(colour);
        }

        [Test]
        public void Builder_AfterReset_HasNoStateLeft()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("Test").SetUnit("W");
            IExtendedDataDefinition status = builder.Build();

            builder.Reset();

            Assert.That(builder._definition, Is.Not.SameAs(status));
        }


        [Test]
        public void Builder_AfterBuildAndReset_HasNoStateLeft()
        {
            var builder = new ExtendedDataDefinitionBuilder().SetLabel("Test").SetUnit("W");
            IExtendedDataDefinition status = builder.BuildAndReset();

            Assert.That(builder._definition, Is.Not.SameAs(status));
        }
    }
}
