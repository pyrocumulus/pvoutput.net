using System.Net.Http;

namespace PVOutput.Net.Requests
{
    internal interface IHttpClientProvider
    {
        HttpClient GetHttpClient();
    }
}
