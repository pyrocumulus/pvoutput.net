using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Modules.Implementations;
using PVOutput.Net.Requests.Modules;

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public class PostSystemRequestTests
    {
        private PostSystemRequest CreateRequestWithDefinition(IExtendedDataDefinition definition)
        {
            return new PostSystemRequest() { DataDefinitions = new List<IExtendedDataDefinition>() { definition } };
        }

        [Test]
        public void Parameter_SystemName_CreatesCorrectUriParameters()
        {
            var request = new PostSystemRequest() { SystemName = "New name" };
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual("New name", parameters["name"]);
        }

        [Test]
        public void Parameter_SystemId_CreatesCorrectUriParameters()
        {
            var request = new PostSystemRequest() { SystemId = 54321 };
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(54321, parameters["sid"]);
        }

        public static IEnumerable DefinitionLabelTests
        {
            get
            {
                yield return new TestCaseData(ExtendedDataIndex.v7, "New label", "v7l");
                yield return new TestCaseData(ExtendedDataIndex.v8, "New label", "v8l");
                yield return new TestCaseData(ExtendedDataIndex.v9, "New label", "v9l");
                yield return new TestCaseData(ExtendedDataIndex.v10, "New label", "v10l");
                yield return new TestCaseData(ExtendedDataIndex.v11, "New label", "v11l");
                yield return new TestCaseData(ExtendedDataIndex.v12, "New label", "v12l");
            }
        }

        [Test]
        [TestCaseSource(typeof(PostSystemRequestTests), "DefinitionLabelTests")]
        public void Parameter_DefinitionLabel_CreatesCorrectUriParameters(ExtendedDataIndex index, string label, string parameterKey)
        {
            var request = CreateRequestWithDefinition(new ExtendedDataDefinition() { Index = index, Label = label });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(label, parameters[parameterKey]);
        }

        public static IEnumerable DefinitionUnitTests
        {
            get
            {
                yield return new TestCaseData(ExtendedDataIndex.v7, "Unit", "v7u");
                yield return new TestCaseData(ExtendedDataIndex.v8, "Unit", "v8u");
                yield return new TestCaseData(ExtendedDataIndex.v9, "Unit", "v9u");
                yield return new TestCaseData(ExtendedDataIndex.v10, "Unit", "v10u");
                yield return new TestCaseData(ExtendedDataIndex.v11, "Unit", "v11u");
                yield return new TestCaseData(ExtendedDataIndex.v12, "Unit", "v12u");
            }
        }

        [Test]
        [TestCaseSource(typeof(PostSystemRequestTests), "DefinitionUnitTests")]
        public void Parameter_DefinitionUnit_CreatesCorrectUriParameters(ExtendedDataIndex index, string unit, string parameterKey)
        {
            var request = CreateRequestWithDefinition(new ExtendedDataDefinition() { Index = index, Unit = unit });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(unit, parameters[parameterKey]);
        }

        public static IEnumerable DefinitionAxisTests
        {
            get
            {
                yield return new TestCaseData(ExtendedDataIndex.v7, 0, "v7a");
                yield return new TestCaseData(ExtendedDataIndex.v8, 1, "v8a");
                yield return new TestCaseData(ExtendedDataIndex.v9, 2, "v9a");
                yield return new TestCaseData(ExtendedDataIndex.v10, 3, "v10a");
                yield return new TestCaseData(ExtendedDataIndex.v11, 4, "v11a");
                yield return new TestCaseData(ExtendedDataIndex.v12, 5, "v12a");
            }
        }

        [Test]
        [TestCaseSource(typeof(PostSystemRequestTests), "DefinitionAxisTests")]
        public void Parameter_DefinitionAxis_CreatesCorrectUriParameters(ExtendedDataIndex index, int axis, string parameterKey)
        {
            var request = CreateRequestWithDefinition(new ExtendedDataDefinition() { Index = index, Axis = axis});
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(axis, parameters[parameterKey]);
        }

        public static IEnumerable DefinitionColourTests
        {
            get
            {
                yield return new TestCaseData(ExtendedDataIndex.v7, "abcdef", "v7c");
                yield return new TestCaseData(ExtendedDataIndex.v8, "abcdef", "v8c");
                yield return new TestCaseData(ExtendedDataIndex.v9, "abcdef", "v9c");
                yield return new TestCaseData(ExtendedDataIndex.v10, "abcdef", "v10c");
                yield return new TestCaseData(ExtendedDataIndex.v11, "abcdef", "v11c");
                yield return new TestCaseData(ExtendedDataIndex.v12, "abcdef", "v12c");
            }
        }

        [Test]
        [TestCaseSource(typeof(PostSystemRequestTests), "DefinitionColourTests")]
        public void Parameter_DefinitionColour_CreatesCorrectUriParameters(ExtendedDataIndex index, string colour, string parameterKey)
        {
            var request = CreateRequestWithDefinition(new ExtendedDataDefinition() { Index = index, Colour = colour });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(colour, parameters[parameterKey]);
        }

        public static IEnumerable DefinitionDisplayTypeTests
        {
            get
            {
                yield return new TestCaseData(ExtendedDataIndex.v7,  ExtendedDataDisplayType.Line, "v7g");
                yield return new TestCaseData(ExtendedDataIndex.v8,  ExtendedDataDisplayType.Line, "v8g");
                yield return new TestCaseData(ExtendedDataIndex.v9,  ExtendedDataDisplayType.Line, "v9g");
                yield return new TestCaseData(ExtendedDataIndex.v10, ExtendedDataDisplayType.Line, "v10g");
                yield return new TestCaseData(ExtendedDataIndex.v11, ExtendedDataDisplayType.Line, "v11g");
                yield return new TestCaseData(ExtendedDataIndex.v12, ExtendedDataDisplayType.Line, "v12g");
                yield return new TestCaseData(ExtendedDataIndex.v7,  ExtendedDataDisplayType.Area, "v7g");
                yield return new TestCaseData(ExtendedDataIndex.v8,  ExtendedDataDisplayType.Area, "v8g");
                yield return new TestCaseData(ExtendedDataIndex.v9,  ExtendedDataDisplayType.Area, "v9g");
                yield return new TestCaseData(ExtendedDataIndex.v10, ExtendedDataDisplayType.Area, "v10g");
                yield return new TestCaseData(ExtendedDataIndex.v11, ExtendedDataDisplayType.Area, "v11g");
                yield return new TestCaseData(ExtendedDataIndex.v12, ExtendedDataDisplayType.Area, "v12g");
            }
        }

        [Test]
        [TestCaseSource(typeof(PostSystemRequestTests), "DefinitionDisplayTypeTests")]
        public void Parameter_DefinitionDisplayType_CreatesCorrectUriParameters(ExtendedDataIndex index, ExtendedDataDisplayType displayType, string parameterKey)
        {
            var request = CreateRequestWithDefinition(new ExtendedDataDefinition() { Index = index, DisplayType = displayType });
            var parameters = request.GetUriPathParameters();
            Assert.AreEqual(displayType.ToString(), parameters[parameterKey]);
        }
    }
}
