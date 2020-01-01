using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Favourite
{
    [TestFixture]
    public class FavouriteServiceTests
    {
        [Test]
        public async Task FavouriteService_GetSingle()
        {
            var client = TestUtility.GetMockClient(FavouriteTestsData.GETFAVOURITE_URL, FavouriteTestsData.FAVOURITE_RESPONSE_SINGLE);
            var response = await client.Favourite.GetFavourites();

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsTrue(response.IsSuccess);

            var favourites = response.Values;
            var favourite = favourites.First();

            Assert.AreEqual(21, favourite.SystemId);
            Assert.AreEqual("PVOutput Demo", favourite.SystemName);
            Assert.AreEqual(2450, favourite.SystemSize);
            Assert.AreEqual(2199, favourite.Postcode);
            Assert.AreEqual(14 , favourite.NumberOfPanels);
            Assert.AreEqual(175 , favourite.PanelPower);
            Assert.AreEqual("Enertech", favourite.PanelBrand);
            Assert.AreEqual(1 , favourite.NumberOfInverters);
            Assert.AreEqual(2000, favourite.InverterPower);
            Assert.AreEqual("CMS", favourite.InverterBrand);
            Assert.AreEqual("N" , favourite.Orientation);
            Assert.AreEqual(30.5d, favourite.ArrayTilt);
            Assert.AreEqual("No", favourite.Shade);
            Assert.AreEqual(new DateTime(2010, 1, 1), favourite.InstallDate);
            Assert.AreEqual(-33.907725d, favourite.Latitude);
            Assert.AreEqual(151.026108d, favourite.Longitude);
            Assert.AreEqual(5 , favourite.StatusInterval);
        }

    }
}
