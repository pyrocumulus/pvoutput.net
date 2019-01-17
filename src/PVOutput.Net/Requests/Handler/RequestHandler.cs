using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Requests.Base;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tavis.UriTemplates;

namespace PVOutput.Net.Requests.Handler
{
    internal class RequestHandler
    {
        private readonly PVOutputClient _client;

        public RequestHandler(PVOutputClient client)
        {
            _client = client;
        }

        internal async Task<PVOutputResponse<TResponseContentType>> ExecuteSingleItemRequestAsync<TResponseContentType>(IRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(CreateRequestMessage(request), cancellationToken).ConfigureAwait(false);

                Stream responseStream = await GetResponseContentStreamAsync(responseMessage).ConfigureAwait(false);

                var reader = StringFactoryContainer.CreateObjectReader<TResponseContentType>();

                TResponseContentType content = await reader.ReadObjectAsync(responseStream, cancellationToken);

                return new PVOutputResponse<TResponseContentType>()
                {
                    IsSuccess = true,
                    HasValue = true,
                    ApiRateInformation = GetApiRateInformationfromResponse(responseMessage),
                    Value = content
                };
            }
            catch (Exception ex)
            {
                if (_client.ThrowResponseExceptions)
                    throw;

                return new PVOutputResponse<TResponseContentType>() { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        internal async Task<PVOutputArrayResponse<TResponseContentType>> ExecuteArrayRequestAsync<TResponseContentType>(IRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await ExecuteRequestAsync(CreateRequestMessage(request), cancellationToken).ConfigureAwait(false);

                Stream responseStream = await GetResponseContentStreamAsync(responseMessage).ConfigureAwait(false);

                var reader = StringFactoryContainer.CreateArrayReader<TResponseContentType>();

                IEnumerable<TResponseContentType> content = await reader.ReadArrayAsync(responseStream, cancellationToken);

                return new PVOutputArrayResponse<TResponseContentType>()
                {
                    IsSuccess = true,
                    HasValue = true,
                    ApiRateInformation = GetApiRateInformationfromResponse(responseMessage),
                    Value = content
                };
            }
            catch (Exception ex)
            {
                if (_client.ThrowResponseExceptions)
                    throw;

                return new PVOutputArrayResponse<TResponseContentType>() { IsSuccess = false, Exception = ex };
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private PVOutputApiRateInformation GetApiRateInformationfromResponse(HttpResponseMessage responseMessage)
        {
            var result = new PVOutputApiRateInformation();

            if (responseMessage.Headers.Contains("X-Rate-Limit-Remaining"))
                result.LimitRemaining = Convert.ToInt32(responseMessage.Headers.GetValues("X-Rate-Limit-Remaining").First());
            if (responseMessage.Headers.Contains("X-Rate-Limit-Limit"))
                result.CurrentLimit = Convert.ToInt32(responseMessage.Headers.GetValues("X-Rate-Limit-Limit").First());
            if (responseMessage.Headers.Contains("X-Rate-Limit-Reset"))
                result.LimitResetAt = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(responseMessage.Headers.GetValues("X-Rate-Limit-Reset").First())).UtcDateTime;

            return result;
        }

        private Task<Stream> GetResponseContentStreamAsync(HttpResponseMessage response)
        {
            return response.Content != null ? response.Content.ReadAsStreamAsync() : Task.FromResult(default(Stream));
        }

        private HttpRequestMessage CreateRequestMessage(IRequest request)
        {
            return new HttpRequestMessage(HttpMethod.Get, CreateUrl(request));
        }

        private string CreateUrl(IRequest request)
        {
            var requestTemplate = new UriTemplate(request.UriTemplate);

            foreach (KeyValuePair<string, object> parameter in request.GetUriPathParameters())
                requestTemplate.AddParameter(parameter.Key, parameter.Value);

            string apiUri = requestTemplate.Resolve();
            return $"{_client.PVOutputBaseUri}{apiUri}";
        }

        internal Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _client.HttpClientProvider.GetHttpClient().SendAsync(requestMessage, cancellationToken);
        }
    }
}
