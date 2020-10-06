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
                                { "ll", "85.322520,31.400980" }
                            })
                        .RespondPlainText("");

            var response = await client.Search.SearchByDistanceAsync(new PVCoordinate(85.32252m, 31.40098m), 11);
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
        [TestCase("South Africa 1235", 1235, "South Africa")]
        [TestCase("Australia 3502", 3502, "Australia")]
        [TestCase("Sweden", 0, "Sweden")]
        [TestCase("2560", 2560, "")]
        public void LocationInformation_Parsing_GetsCorrectInformation(string inputString, int expectedPostcode, string expectedCountry)
        {
            SystemSearchResultObjectStringReader.SplitPostCode(inputString, out int postcode, out string country);
            Assert.That(postcode, Is.EqualTo(expectedPostcode));
            Assert.That(country, Is.EqualTo(expectedCountry));
        }

        [Test]
        public async Task SearchReader_ForResponse_CreatesCorrectObject()
        {
            ISystemSearchResult result = await TestUtility.ExecuteObjectReaderByTypeAsync<ISystemSearchResult>(SEARCH_RESPONSE_SINGLE);

            Assert.Multiple(() =>
            {
                // "Solar 4 US,9360,4280,NW,81,2 days ago,249,Solarfun,Aurora,NaN,-27.831402,153.028469"
                Assert.That(result.SystemName, Is.EqualTo("Solar 4 US"));
                Assert.That(result.SystemSize, Is.EqualTo(9360));
                Assert.That(result.Postcode, Is.EqualTo(4280));
                Assert.That(result.Country, Is.EqualTo("Australia"));
                Assert.That(result.Orientation, Is.EqualTo("NW"));
                Assert.That(result.NumberOfOutputs, Is.EqualTo(81));
                Assert.That(result.LastOutput, Is.EqualTo("2 days ago"));
                Assert.That(result.SystemId, Is.EqualTo(249));
                Assert.That(result.Panel, Is.EqualTo("Solarfun"));
                Assert.That(result.Inverter, Is.EqualTo("Aurora"));
                Assert.That(result.Distance, Is.Null);
                Assert.That(result.Location.Latitude, Is.EqualTo(-27.831402));
                Assert.That(result.Location.Longitude, Is.EqualTo(153.028469));
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
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Exactly(4).Items);

                Assert.That(first.SystemName, Is.EqualTo("Solar 4 US"));
                Assert.That(last.SystemName, Is.EqualTo("solar powered muso"));

                Assert.That(first.Location.Latitude, Is.EqualTo(-27.831402));
                Assert.That(first.Location.Longitude, Is.EqualTo(153.028469));
                Assert.That(last.Location.Latitude, Is.EqualTo(-34.878302));
                Assert.That(last.Location.Longitude, Is.EqualTo(138.663553));
            });
        }
    }
}
