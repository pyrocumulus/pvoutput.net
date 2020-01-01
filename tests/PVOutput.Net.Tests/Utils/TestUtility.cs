using PVOutput.Net.Tests.Requests.Handler;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Utils
{
    public static class TestUtility
    {
        public static PVOutputClient GetMockClient(string uri, string mockResponseContent)
        {
            var provider = new TestHttpClientProvider();
            provider.When(uri, mockResponseContent);
            provider.MockHttpMessageHandler.Fallback.RespondPlainText("");
            return new PVOutputClient(TestConstants.PVOUTPUT_API_KEY, TestConstants.PVOUTPUT_SYSTEM_ID, provider);
        }

        public static PVOutputClient GetMockClient(out MockHttpMessageHandler mockHandler)
        {
            var provider = new TestHttpClientProvider();
            mockHandler = provider.MockHttpMessageHandler;
            mockHandler.Fallback.RespondPlainText("");
            return new PVOutputClient(TestConstants.PVOUTPUT_API_KEY, TestConstants.PVOUTPUT_SYSTEM_ID, provider);
        }
    }
}
