using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.Insolation
{
    [TestFixture]
    public class InsolationServiceTests
    {
        [Test]
        public async Task InsolationService_GetBasic()
        {
            var client = TestUtility.GetMockClient(InsolationServiceTestsData.GETINSOLATION_URL, InsolationServiceTestsData.INSOLATION_RESPONSE_BASIC);
            var response = await client.Insolation.GetInsolationForSystem(12345);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValues);
            Assert.IsTrue(response.IsSuccess);

            var insolations = response.Values;
            var insolation = insolations.First();

            Assert.AreEqual(0, insolation.Energy);
            Assert.AreEqual(0, insolation.Power);
            Assert.AreEqual(DateTime.Today.Add(new TimeSpan(6, 0, 0)), insolation.Time);

            insolation = insolations.Last();

            Assert.AreEqual(30, insolation.Energy);
            Assert.AreEqual(123, insolation.Power);
            Assert.AreEqual(DateTime.Today.Add(new TimeSpan(6, 35, 0)), insolation.Time);
        }
    }
}
