using PVOutput.Net.Tests.Requests.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Utils
{
    public static class TestUtility
    {
        public static PVOutputClient GetMockClient(string uri, string mockResponseContent)
        {
            var provider = new TestHttpClientProvider();
            provider.SetupMockResponse(uri, mockResponseContent);
            return new PVOutputClient(TestConstants.PVOUTPUT_API_KEY, TestConstants.PVOUTPUT_SYSTEM_ID, provider);
        }
    }
}
