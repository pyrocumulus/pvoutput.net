using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Tests.Utils;

namespace PVOutput.Net.Tests.Modules.System
{
    [TestFixture]
    public class TeamServiceTests
    {
        [Test]
        public async Task TeamService_GetMcDonalds()
        {
            var client = TestUtility.GetMockClient(TeamTestsData.GETTEAM_URL, TeamTestsData.TEAM_RESPONSE_SIMPLE);
            var response = await client.Team.GetTeamAsync(350);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            Assert.IsTrue(response.HasValue);
            Assert.IsNotNull(response.IsSuccess);

            var team = response.Value;
            Assert.AreEqual("McDonalds Android App", team.Name);
            Assert.AreEqual("Join the team if you use the app.", team.Description);
            Assert.AreEqual("Software", team.Type);

            Assert.AreEqual(8554332, team.TeamSize);
            Assert.AreEqual(5741, team.AverageSize);

            Assert.AreEqual(1490, team.NumberOfSystems);
            Assert.AreEqual(45566122184, team.EnergyGenerated);
            Assert.AreEqual(2521753, team.Outputs);
            Assert.AreEqual(17851, team.EnergyAverage);
            Assert.AreEqual(new DateTime(2012, 2, 19), team.CreationDate);
        }
    }
}
