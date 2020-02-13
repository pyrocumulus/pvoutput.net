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
            var client = TestUtility.GetMockClient(out var testProvider);

            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .WithHeaders(new Dictionary<string, string>()
                        {
                            { "X-Pvoutput-Apikey", TestConstants.PVOUTPUT_API_KEY },
                            { "X-Pvoutput-SystemId", TestConstants.PVOUTPUT_SYSTEM_ID.ToString() },
                            { "X-Rate-Limit", "1" }
                        })
                        .RespondPlainText("");

            _ = await client.System.GetOwnSystem();
            testProvider.VerifyNoOutstandingExpectation();
        }

        [Test]
        public void DefaultClient_OnErrorResponse_ThrowsException()
        {

            const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
            const string responseContent = "Invalid API Key";

            var client = TestUtility.GetMockClient(out var testProvider);
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            var exception = Assert.ThrowsAsync<PVOutputException>(async () =>
            {
                _ = await client.System.GetOwnSystem();
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

            var client = TestUtility.GetMockClient(out var testProvider);
            client.ThrowResponseExceptions = false;
            testProvider.ExpectUriFromBase("getsystem.jsp")
                        .Respond(statusCode, "text/plain", responseContent);

            var response = await client.System.GetOwnSystem();

            Assert.AreEqual(responseContent, response.Error.Message);
            Assert.AreEqual(statusCode, response.Error.StatusCode);

            testProvider.VerifyNoOutstandingExpectation();
        }
    }
}
