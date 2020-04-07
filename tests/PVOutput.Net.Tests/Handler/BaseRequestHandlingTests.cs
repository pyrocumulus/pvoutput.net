using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
using PVOutput.Net.Responses;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Handler
{
    [TestFixture]
    public class BaseRequestHandlingTests
    {
        [Test]
        public async Task RequestHandler_OnRequest_SetsRequiredHeaders()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .WithHeaders(new Dictionary<string, string>()
                        {
                            { "X-Pvoutput-Apikey", TestConstants.PVOUTPUT_API_KEY },
                            { "X-Pvoutput-SystemId", TestConstants.PVOUTPUT_SYSTEM_ID.ToString() },
                            { "X-Rate-Limit", "1" }
                        })
                        .RespondPlainText("");

            var response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();
            Assert.IsTrue(response.ToBoolean());
        }

        [Test]
        public void DefaultClient_OnErrorResponse_ThrowsException()
        {

            const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            const string responseContent = "Invalid API Key";

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            var exception = Assert.ThrowsAsync<PVOutputException>(async () =>
            {
                _ = await client.System.GetOwnSystemAsync();
            });
            Assert.AreEqual(responseContent, exception.Message);
            Assert.AreEqual(statusCode, exception.StatusCode);

            testProvider.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task ClientWithNoThrowOption_OnErrorResponse_ReturnsErrorResponse()
        {
            const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            const string responseContent = "Invalid API Key";

            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            var response = await client.System.GetOwnSystemAsync();

            Assert.IsFalse(response);
            Assert.AreEqual(responseContent, response.Error.Message);
            Assert.AreEqual(statusCode, response.Error.StatusCode);

            testProvider.VerifyNoOutstandingExpectation();
        }

        [Test]
        public async Task Response_WithApiRateInformation_ParsesCorrectInformation()
        {
            PVOutputClient client = TestUtility.GetMockClient(out MockHttpMessageHandler testProvider);

            var resetTimeStamp = new DateTime(2020, 3, 15, 11, 20, 0);
            var offset = (DateTimeOffset)DateTime.SpecifyKind(resetTimeStamp, DateTimeKind.Utc);

            var responseHeaders = new Dictionary<string, string>()
                        {
                            { "X-Rate-Limit-Remaining", "156" },
                            { "X-Rate-Limit-Limit", "300" },
                            { "X-Rate-Limit-Reset", offset.ToUnixTimeSeconds().ToString() }
                        };

            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(responseHeaders, "text/plain", "");

            var response = await client.System.GetOwnSystemAsync();
            testProvider.VerifyNoOutstandingExpectation();

            Assert.Multiple(() => {
                Assert.AreEqual(156, response.ApiRateInformation.LimitRemaining);
                Assert.AreEqual(300, response.ApiRateInformation.CurrentLimit);
                Assert.AreEqual(resetTimeStamp, response.ApiRateInformation.LimitResetAt);
            });
        }
    }
}
