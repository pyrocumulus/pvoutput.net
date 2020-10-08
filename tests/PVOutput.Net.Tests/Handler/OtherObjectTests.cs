using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using NUnit.Framework;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules.Readers;

namespace PVOutput.Net.Tests.Handler
{
    [TestFixture]
    public class OtherObjectTests
    {
        public static IEnumerable PVCoordinateRegularEqualityTestCases
        {
            get
            {
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(50.1m, 6.1m)).Returns(true);
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(51.0m, 6.1m)).Returns(false);
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(50.1m, 6.2m)).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(PVCoordinateRegularEqualityTestCases))]
        public bool PVCoordinate_Equals_ReturnsValueEquality(PVCoordinate coordinate1, PVCoordinate coordinate2)
        {
            return coordinate1.Equals(coordinate2);
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(PVCoordinateRegularEqualityTestCases))]
        public bool PVCoordinate_GetHashCode_ReturnsEqualHashesForEqualValues(PVCoordinate coordinate1, PVCoordinate coordinate2)
        {
            return coordinate1.GetHashCode() == coordinate2.GetHashCode();
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(PVCoordinateRegularEqualityTestCases))]
        public bool PVCoordinate_OperatorEquals_ReturnsValueEquality(PVCoordinate coordinate1, PVCoordinate coordinate2)
        {
            return coordinate1 == coordinate2;
        }

        public static IEnumerable PVCoordinateInvertedEqualityTestCases
        {
            get
            {
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(50.1m, 6.1m)).Returns(false);
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(51.0m, 6.1m)).Returns(true);
                yield return new TestCaseData(new PVCoordinate(50.1m, 6.1m), new PVCoordinate(50.1m, 6.2m)).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(PVCoordinateInvertedEqualityTestCases))]
        public bool PVCoordinate_OperatorNotEquals_ReturnsValueEquality(PVCoordinate coordinate1, PVCoordinate coordinate2)
        {
            return coordinate1 != coordinate2;
        }

        public static IEnumerable ExtendedDataConfigurationRegularEqualityTestCases
        {
            get
            {
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label", "Unit")).Returns(true);
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label1", "Unit")).Returns(false);
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label", "Unit2")).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(ExtendedDataConfigurationRegularEqualityTestCases))]
        public bool ExtendedDataConfiguration_Equals_ReturnsValueEquality(ExtendedDataConfiguration configuration1, ExtendedDataConfiguration configuration2)
        {
            return configuration1.Equals(configuration2);
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(ExtendedDataConfigurationRegularEqualityTestCases))]
        public bool ExtendedDataConfiguration_GetHashCode_ReturnsEqualHashesForEqualValues(ExtendedDataConfiguration configuration1, ExtendedDataConfiguration configuration2)
        {
            return configuration1.GetHashCode() == configuration2.GetHashCode();
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(ExtendedDataConfigurationRegularEqualityTestCases))]
        public bool ExtendedDataConfiguration_OperatorEquals_ReturnsValueEquality(ExtendedDataConfiguration configuration1, ExtendedDataConfiguration configuration2)
        {
            return configuration1 == configuration2;
        }

        public static IEnumerable ExtendedDataConfigurationInvertedEqualityTestCases
        {
            get
            {
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label", "Unit")).Returns(false);
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label1", "Unit")).Returns(true);
                yield return new TestCaseData(new ExtendedDataConfiguration("Label", "Unit"), new ExtendedDataConfiguration("Label", "Unit2")).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(typeof(OtherObjectTests), nameof(ExtendedDataConfigurationInvertedEqualityTestCases))]
        public bool ExtendedDataConfiguration_OperatorNotEquals_ReturnsValueEquality(ExtendedDataConfiguration configuration1, ExtendedDataConfiguration configuration2)
        {
            return configuration1 != configuration2;
        }

        [Test]
        public void StringFactoryContainer_ForType_CreatesObjectReader()
        {
            IObjectStringReader<IStatus> reader = StringFactoryContainer.CreateObjectReader<IStatus>();
            Assert.That(reader, Is.TypeOf<StatusObjectStringReader>());
        }

        [Test]
        public void StringFactoryContainer_ForType_CreatesArrayReader()
        {
            IArrayStringReader<ISystemSearchResult> reader = StringFactoryContainer.CreateArrayReader<ISystemSearchResult>();
            Assert.That(reader, Is.TypeOf<LineDelimitedArrayStringReader<ISystemSearchResult>>());
        }

        [Test]
        public void StringFactoryContainer_ForUnknownType_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => {
                _ = StringFactoryContainer.CreateObjectReader<IServiceProvider>();
            });
        }

        [Test]
        public void StringFactoryContainer_ForTypeWithoutArrayReader_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => {
                _ = StringFactoryContainer.CreateArrayReader<IStatus>();
            });
        }
    }
}
