using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Modules;
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
        public async Task SearchService_WithQuery_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(SEARCH_URL)
                        .RespondPlainText("");

            var response = await client.Search.SearchAsync("test query");
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public void SearchService_Search_WithNullOrEmptyQuery_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                _ = await client.Search.SearchAsync(searchQuery: null);
            });
        }


        [Test]
        public void SearchService_Search_WithEmptyQuery_Throws()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                _ = await client.Search.SearchAsync("");
            });
        }

        [Test]
        public async Task SearchService_SearchByNameStartsWith_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByNameAsync("name", true), "name%2A");
        }

        [Test]
        public async Task SearchService_SearchByNameContains_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByNameAsync("name", false), "name");
        }

        [Test]
        public async Task SearchService_SearchByPostcodeOrSize_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByPostcodeOrSizeAsync(2500), "2500");
        }

        [Test]
        public async Task SearchService_SearchByPostcode_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByPostcodeAsync("XE"), "XE postcode");
        }

        [Test]
        public async Task SearchService_SearchBySize_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchBySizeAsync(4250), "4250 size");
        }

        [Test]
        public async Task SearchService_SearchByInverter_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByInverterAsync("fronius"), "fronius%2A inverter");
        }

        [Test]
        public async Task SearchService_SearchByPanel_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByPanelAsync("sharp"), "sharp panel");
        }

        [Test]
        public async Task SearchService_SearchByTeam_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByTeamAsync("mcdonalds"), "mcdonalds team");
        }

        [Test]
        public async Task SearchService_GetFavourites_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.GetFavouritesAsync(), "favourite");
        }

        [Test]
        public async Task SearchService_SearchByOrientationAndNameStartsWith_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByOrientationAsync(Enums.Orientation.SouthEast, "name", true), "name%2A %2BSE");
        }

        [Test]
        public async Task SearchService_SearchByOrientationAndNameContains_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByOrientationAsync(Enums.Orientation.NorthWest, "name", false), "name %2BNW");
        }

        [Test]
        public async Task SearchService_SearchByTilt_CallsCorrectUri()
        {
            await TestSpecificSearchQuery(s => s.SearchByTiltAsync(53), "53 tilt");
        }

        [Test]
        public async Task SearchService_SearchByDistanceWithPostcode_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(SEARCH_URL)
                        .WithQueryString(new Dictionary<string, string>
                            {
                                { "q", "2500 10km" },
                                { "country_code", "nl" }
                            })
                        .RespondPlainText("");

            var response = await client.Search.SearchByDistanceAsync(2500, 10, "nl");
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task SearchService_SearchByDistanceWithCoordinate_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(SEARCH_URL)
                        .WithQueryString(new Dictionary<string, string>
                            {
                                { "q", "11km" },
                                { "ll", "85.32252,31.40098" }
                            })
                        .RespondPlainText("");

            var response = await client.Search.SearchByDistanceAsync(new PVCoordinate(85.32252, 31.40098), 11);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        private async Task TestSpecificSearchQuery(Func<SearchService, Task<Responses.PVOutputArrayResponse<ISystemSearchResult>>> searchQuery, string queryStringResult)
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase(SEARCH_URL)
                        .WithQueryString(new Dictionary<string, string>
                            {
                                { "q", queryStringResult }
                            })
                        .RespondPlainText("");

            var response = await searchQuery(client.Search);
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
