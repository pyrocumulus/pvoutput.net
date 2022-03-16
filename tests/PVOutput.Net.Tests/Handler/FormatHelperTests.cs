using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;

namespace PVOutput.Net.Tests.Handler
{
    [TestFixture]
    public class FormatHelperTests
    {
        [Test]
        public void GetEnumerationDescription_ForInvalidType_Throws()
        {
            Assert.Throws<ArgumentException>(() => {
                _ = FormatHelper.GetEnumerationDescription(0);
            });
        }

        [Test]
        public void GetEnumerationDescription_ForEnum_GetsDescription()
        {
            string result = FormatHelper.GetEnumerationDescription(WeatherCondition.PartlyCloudy);
            Assert.That(result, Is.EqualTo("Partly Cloudy"));
        }

        [Test]
        public void GetEnumerationDescription_ForEnumWithoutDescription_ReturnsNull()
        {
            string result = FormatHelper.GetEnumerationDescription(PVMonth.December);
            Assert.That(result, Is.Null);
        }


        [Test]
        public void DescriptionToEnumValue_ForDescription_GetsEnumValue()
        {
            var enumValue = FormatHelper.DescriptionToEnumValue<WeatherCondition>("Partly Cloudy");
            Assert.That(enumValue, Is.EqualTo(WeatherCondition.PartlyCloudy));
        }

        [Test]
        public void DescriptionToEnumValue_ForInvalidType_Throws()
        {
            Assert.Throws<ArgumentException>(() => {
                _ = FormatHelper.DescriptionToEnumValue<int>("0");
            });
        }

        [Test]
        public void DescriptionToEnumValue_ForInvalidDescription_Throws()
        {
            Assert.Throws<ArgumentException>(() => {
                _ = FormatHelper.DescriptionToEnumValue<WeatherCondition>("NotFound");
            });
        }


        [Test]
        public void DescriptionToNullableEnumValue_ForDescription_GetsEnumValue()
        {
            var enumValue = FormatHelper.DescriptionToNullableEnumValue<WeatherCondition>("Fine");
            Assert.That(enumValue, Is.EqualTo(WeatherCondition.Fine));
        }

        [Test]
        public void DescriptionToNullableEnumValue_ForInvalidType_ReturnsNull()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var enumValue = FormatHelper.DescriptionToNullableEnumValue<int>("0");
            });
        }

        [Test]
        public void DescriptionToNullableEnumValue_ForInvalidDescription_Throws()
        {
            Assert.Throws<ArgumentException>(() => {
                _ = FormatHelper.DescriptionToNullableEnumValue<WeatherCondition>("NotFound");
            });
        }

        [Test]
        public void DescriptionToNullableEnumValue_ForNullLiteralDescription_ReturnsNull()
        {
            var enumValue = FormatHelper.DescriptionToNullableEnumValue<WeatherCondition>("null");
            Assert.That(enumValue, Is.Null);
        }

        [Test]
        public void ParseOptionalDate_ForEmptyString_ReturnsNull()
        {
            var result = FormatHelper.ParseOptionalDate("");
            Assert.That(result, Is.Null);
        }
    }
}
