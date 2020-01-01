using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Team
{
    [TestFixture]
    public partial class TeamServiceTests
    {
        [Test]
        public async Task TeamService_GetStatusForTeam_CallsCorrectUri()
        {
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase(GETTEAM_URL)
                        .WithQueryString("tid=350")
                        .RespondPlainText(TEAM_RESPONSE_SIMPLE);

            var response = await client.Team.GetTeamAsync(350);
            testProvider.VerifyNoOutstandingExpectation();

            Assert.IsNull(response.Exception);
            Assert.IsTrue(response.HasValue);
            Assert.IsTrue(response.IsSuccess);
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task TeamReader_ForResponse_CreatesCorrectObject()
        {
            var reader = StringFactoryContainer.CreateObjectReader<ITeam>();
            ITeam result = await reader.ReadObjectAsync(new StringReader(TEAM_RESPONSE_SIMPLE));

            Assert.AreEqual("McDonalds Android App", result.Name);
            Assert.AreEqual("Join the team if you use the app.", result.Description);
            Assert.AreEqual("Software", result.Type);

            Assert.AreEqual(8554332, result.TeamSize);
            Assert.AreEqual(5741, result.AverageSize);

            Assert.AreEqual(1490, result.NumberOfSystems);
            Assert.AreEqual(45566122184, result.EnergyGenerated);
            Assert.AreEqual(2521753, result.Outputs);
            Assert.AreEqual(17851, result.EnergyAverage);
            Assert.AreEqual(new DateTime(2012, 2, 19), result.CreationDate);
        }
    }
}
