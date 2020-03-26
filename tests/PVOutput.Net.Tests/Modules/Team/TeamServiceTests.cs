using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Objects;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Modules.Team
{
    [TestFixture]
    public partial class TeamServiceTests : BaseRequestsTest
    {
        [Test]
        public async Task TeamService_GetStatusForTeam_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(GETTEAM_URL)
                        .WithQueryString("tid=350")
                        .RespondPlainText(TEAM_RESPONSE_SIMPLE);

            var response = await client.Team.GetTeamAsync(350);
            testProvider.VerifyNoOutstandingExpectation();
            AssertStandardResponse(response);
        }

        [Test]
        public async Task TeamService_JoinTeam_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(JOINTEAM_URL)
                        .WithQueryString("tid=360")
                        .Respond(HttpStatusCode.OK, "text/plain", "You have joined team [team_name]");

            var response = await client.Team.JoinTeamAsync(360);
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual("You have joined team [team_name]", response.SuccesMessage);
            });
        }

        [Test]
        public async Task TeamService_LeaveTeam_CallsCorrectUri()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase(LEAVETEAM_URL)
                        .WithQueryString("tid=340")
                        .Respond(HttpStatusCode.OK, "text/plain", "You have left team [team_name]");

            var response = await client.Team.LeaveTeamAsync(340);
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccess);
                Assert.AreEqual("You have left team [team_name]", response.SuccesMessage);
            });
        }

        [Test]
        public async Task TeamService_JoinTeamWithInvalidResponse_ReturnsCorrectResponse()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;

            testProvider.ExpectUriFromBase(JOINTEAM_URL)
                        .Respond(HttpStatusCode.BadRequest, "text/plain", "You cannot join a team that does not exist");

            var response = await client.Team.JoinTeamAsync(340);

            Assert.Multiple(() => 
            {
                Assert.IsFalse(response.IsSuccess);
                Assert.AreEqual(HttpStatusCode.BadRequest, response.Error.StatusCode);
                Assert.AreEqual("You cannot join a team that does not exist", response.Error.Message);
            });
        }

        /*
         * Deserialisation tests below
         */

        [Test]
        public async Task TeamReader_ForResponse_CreatesCorrectObject()
        {
            ITeam result = await TestUtility.ExecuteObjectReaderByTypeAsync<ITeam>(TEAM_RESPONSE_SIMPLE);

            Assert.Multiple(() =>
            {
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
            });
        }
    }
}
