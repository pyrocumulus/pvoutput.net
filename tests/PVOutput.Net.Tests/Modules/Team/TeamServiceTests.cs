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
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.SuccesMessage, Is.EqualTo("You have joined team [team_name]"));
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
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.SuccesMessage, Is.EqualTo("You have left team [team_name]"));
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
                Assert.That(response.IsSuccess, Is.False);
                Assert.That(response.Error.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(response.Error.Message, Is.EqualTo("You cannot join a team that does not exist"));
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
                Assert.That(result.Name, Is.EqualTo("McDonalds Android App"));
                Assert.That(result.Description, Is.EqualTo("Join the team if you use the app."));
                Assert.That(result.Type, Is.EqualTo("Software"));
                Assert.That(result.TeamSize, Is.EqualTo(8554332));
                Assert.That(result.AverageSize, Is.EqualTo(5741));
                Assert.That(result.NumberOfSystems, Is.EqualTo(1490));
                Assert.That(result.EnergyGenerated, Is.EqualTo(45566122184));
                Assert.That(result.Outputs, Is.EqualTo(2521753));
                Assert.That(result.EnergyAverage, Is.EqualTo(17851));
                Assert.That(result.CreationDate, Is.EqualTo(new DateTime(2012, 2, 19)));
            });
        }
    }
}
