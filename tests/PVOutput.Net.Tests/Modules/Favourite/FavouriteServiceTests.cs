using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects;
using PVOutput.Net.Responses;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Favourite
{
    [TestFixture]
    public partial class FavouriteServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task FavouriteService_GetSingle()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETFAVOURITE_URL)
                        .RespondPlainText(FAVOURITE_RESPONSE_SINGLE);

            PVOutputArrayResponse<IFavourite> response = await client.Favourite.GetFavouritesAsync();
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task FavouriteReader_ForResponse_CreatesCorrectObjects()
        {
            IEnumerable<IFavourite> result = await TestUtility.ExecuteArrayReaderByTypeAsync<IFavourite>(FAVOURITE_RESPONSE_SINGLE);

            IFavourite favourite = result.First();
            Assert.Multiple(() => {  
                Assert.That(favourite.SystemId, Is.EqualTo(21));
                Assert.That(favourite.SystemName, Is.EqualTo("PVOutput Demo"));
                Assert.That(favourite.SystemSize, Is.EqualTo(2450));
                Assert.That(favourite.Postcode, Is.EqualTo(2199));
                Assert.That(favourite.NumberOfPanels, Is.EqualTo(14));
                Assert.That(favourite.PanelPower, Is.EqualTo(175));
                Assert.That(favourite.PanelBrand, Is.EqualTo("Enertech"));
                Assert.That(favourite.NumberOfInverters, Is.EqualTo(1));
                Assert.That(favourite.InverterPower, Is.EqualTo(2000));
                Assert.That(favourite.InverterBrand, Is.EqualTo("CMS"));
                Assert.That(favourite.Orientation, Is.EqualTo(Orientation.North));
                Assert.That(favourite.ArrayTilt, Is.EqualTo(30.5d));
                Assert.That(favourite.Shade, Is.EqualTo(Shade.None));
                Assert.That(favourite.InstallDate, Is.EqualTo(new DateTime(2010, 1, 1)));
                Assert.That(favourite.Location.Latitude, Is.EqualTo(-33.907725d));
                Assert.That(favourite.Location.Longitude, Is.EqualTo(151.026108d));
                Assert.That(favourite.StatusInterval, Is.EqualTo(5));
            });
        }

    }
}
