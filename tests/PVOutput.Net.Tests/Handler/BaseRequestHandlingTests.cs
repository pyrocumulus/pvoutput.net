using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PVOutput.Net.Enums;
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
    }
}
