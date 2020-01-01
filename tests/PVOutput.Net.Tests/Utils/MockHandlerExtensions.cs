using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Tests.Utils;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Utils
{
    internal static class MockHandlerExtensions
    {
        internal static MockedRequest ExpectUriFromBase(this MockHttpMessageHandler handler, string uri)
        {
            return handler.Expect(TestConstants.BASE_URL + uri);
        }

        internal static MockedRequest RespondPlainText(this MockedRequest request, string responseContent)
        {
            return request.Respond("text/plain", responseContent);
        }
    }
}
