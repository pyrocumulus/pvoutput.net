using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Modules.Readers;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;


namespace PVOutput.Net.Tests.Modules.Search
{
    [TestFixture]
    public partial class SearchServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task SearchService_WithNoParameters_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(SEARCH_URL)
                        .RespondPlainText("");

            var response = await client.Search.SearchAsync("");
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        [TestCase(new object[] { "South Africa 1235", 1235, "South Africa" })]
        [TestCase(new object[] { "Australia 3502", 3502, "Australia" })]
        [TestCase(new object[] { "Sweden", 0, "Sweden" })]
        [TestCase(new object[] { "2560", 2560, "" })]
        public void LocationInformation_Parsing_GetsCorrectInformation(string inputString, int expectedPostcode, string expectedCountry)
        {
            SystemSearchResultObjectStringReader.SplitPostCode(inputString, out int postcode, out string country);
            Assert.AreEqual(expectedPostcode, postcode);
            Assert.AreEqual(expectedCountry, country);
        }

        [Test]
        public async Task SearchReader_ForResponse_CreatesCorrectObject()
        {
            ISystemSearchResult result = await TestUtility.ExecuteObjectReaderByTypeAsync<ISystemSearchResult>(SEARCH_RESPONSE_SINGLE);

            Assert.Multiple(() =>
            {
                // "Solar 4 US,9360,4280,NW,81,2 days ago,249,Solarfun,Aurora,NaN,-27.831402,153.028469"
                Assert.AreEqual("Solar 4 US", result.SystemName);
                Assert.AreEqual(9360, result.SystemSize);
                Assert.AreEqual(4280, result.Postcode);
                Assert.AreEqual("NW", result.Orientation);
                Assert.AreEqual(81, result.NumberOfOutputs);
                Assert.AreEqual("2 days ago", result.LastOutput);
                Assert.AreEqual(249, result.SystemId);
                Assert.AreEqual("Solarfun", result.Panel);
                Assert.AreEqual("Aurora", result.Inverter);
                Assert.IsNull(result.Distance);
                Assert.AreEqual(-27.831402, result.Location.Latitude);
                Assert.AreEqual(153.028469, result.Location.Longitude);
            });
        }

        [Test]
        public async Task SearchReader_ForResponse_CreatesCorrectObjects()
        {
            IEnumerable<ISystemSearchResult> result = await TestUtility.ExecuteArrayReaderByTypeAsync<ISystemSearchResult>(SEARCH_RESPONSE_ARRAY);

            var first = result.First();
            var last = result.Last();

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(4, result.Count());

                Assert.AreEqual("Solar 4 US", first.SystemName);
                Assert.AreEqual("solar powered muso", last.SystemName);

                Assert.AreEqual(-27.831402, first.Location.Latitude);
                Assert.AreEqual(153.028469, first.Location.Longitude);
                Assert.AreEqual(-34.878302, last.Location.Latitude);
                Assert.AreEqual(138.663553, last.Location.Longitude);
            });
        }
    }
}
