using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Tests.Requests.Handler;
using RichardSzalay.MockHttp;

namespace PVOutput.Net.Tests.Utils
{
    public static class TestUtility
    {
        /// <summary>
        /// Creates and executes an object reader for a given type
        /// </summary>
        /// <typeparam name="TContentResponseType">Interface to create a reader for</typeparam>
        /// <param name="response">The content to create an object for</param>
        /// <returns>The returned object from the reader</returns>
        public static async Task<TContentResponseType> ExecuteObjectReaderByTypeAsync<TContentResponseType>(string response)
        {
            var reader = StringFactoryContainer.CreateObjectReader<TContentResponseType>();
            return await reader.ReadObjectAsync(new StringReader(response));
        }

        /// <summary>
        /// Creates and executes an array reader for a given type
        /// </summary>
        /// <typeparam name="TContentResponseType"></typeparam>
        /// <param name="response">The content to create an array for</param>
        /// <returns>The returned array from the reader</returns>
        public static async Task<IEnumerable<TContentResponseType>> ExecuteArrayReaderByTypeAsync<TContentResponseType>(string response)
        {
            var reader = StringFactoryContainer.CreateArrayReader<TContentResponseType>();
            return await reader.ReadArrayAsync(new StringReader(response));
        }

        public static PVOutputClient GetMockClient(out MockHttpMessageHandler mockHandler)
        {
            var provider = new TestHttpClientProvider();
            mockHandler = provider.MockHttpMessageHandler;
            mockHandler.Fallback.RespondPlainText("");
            var client = new PVOutputClient(TestConstants.PVOUTPUT_API_KEY, TestConstants.PVOUTPUT_SYSTEM_ID, new TestOutputLogger());
            client.HttpClientProvider = provider;
            return client;
        }
    }
}
