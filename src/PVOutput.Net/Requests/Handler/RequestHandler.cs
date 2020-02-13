using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Factories;
using PVOutput.Net.Requests.Base;
using PVOutput.Net.Responses;
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
                using (HttpRequestMessage requestMessage = CreateRequestMessage(request))
                {
                    responseMessage = await ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                }
                Stream responseStream = await GetResponseContentStreamAsync(responseMessage).ConfigureAwait(false);

                var result = new PVOutputResponse<TResponseContentType>();
                result.ApiRateInformation = GetApiRateInformationfromResponse(responseMessage);

                if (ResponseIsErrorResponse(responseMessage, responseStream, result))
                {
                    return result;
                }

                Objects.Core.IObjectStringReader<TResponseContentType> reader = StringFactoryContainer.CreateObjectReader<TResponseContentType>();
                TResponseContentType content = await reader.ReadObjectAsync(responseStream, cancellationToken).ConfigureAwait(false);

                result.IsSuccess = true;
                result.Value = content;
                return result;
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
                using (HttpRequestMessage requestMessage = CreateRequestMessage(request))
                {
                    responseMessage = await ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                }
                Stream responseStream = await GetResponseContentStreamAsync(responseMessage).ConfigureAwait(false);

                var result = new PVOutputArrayResponse<TResponseContentType>();
                result.ApiRateInformation = GetApiRateInformationfromResponse(responseMessage);

                if (ResponseIsErrorResponse(responseMessage, responseStream, result))
                { 
                    return result;
                }

                IArrayStringReader<TResponseContentType> reader = StringFactoryContainer.CreateArrayReader<TResponseContentType>();
                IEnumerable<TResponseContentType> content = await reader.ReadArrayAsync(responseStream, cancellationToken).ConfigureAwait(false);

                result.IsSuccess = true;
                result.Values = content;
                return result;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        internal async Task<PVOutputBasicResponse> ExecutePostRequestAsync(IRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                using (HttpRequestMessage requestMessage = CreateRequestMessage(request))
                {
                    responseMessage = await ExecuteRequestAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                }
                Stream responseStream = await GetResponseContentStreamAsync(responseMessage).ConfigureAwait(false);

                var result = new PVOutputBasicResponse();
                result.ApiRateInformation = GetApiRateInformationfromResponse(responseMessage);

                if (ResponseIsErrorResponse(responseMessage, responseStream, result))
                {
                    return result;
                }

                result.IsSuccess = true;
                result.SuccesMessage = GetBasicResponseState(responseStream);
                return result;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        private bool ResponseIsErrorResponse(HttpResponseMessage responseMessage, Stream responseStream, PVOutputBaseResponse result)
        {
            PVOutputApiError apiError = ProcessHttpErrorResults(responseMessage, responseStream);
            if (apiError != null)
            {
                result.IsSuccess = false;
                result.Error = apiError;
                return true;
            }
            return false;
        }

        private PVOutputApiError ProcessHttpErrorResults(HttpResponseMessage response, Stream responseStream)
        {
            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var error = new PVOutputApiError();
            error.StatusCode = response.StatusCode;
            using (TextReader textReader = new StreamReader(responseStream))
            {
                var fullContent = textReader.ReadToEnd();

                if (!string.IsNullOrEmpty(fullContent))
                {
                    var splitterIndex = fullContent.IndexOf(':');

                    if (splitterIndex > -1)
                    {
                        error.Message = fullContent.Substring(splitterIndex + 1).Trim();
                    }
                    else
                    {
                        error.Message = fullContent;
                    }
                }
            }

            if (_client.ThrowResponseExceptions)
            {
                throw new PVOutputException(error.StatusCode, error.Message);
            }

            return error;
        }

        private static string GetBasicResponseState(Stream responseStream)
        {
            using (TextReader textReader = new StreamReader(responseStream))
            {
                var fullContent = textReader.ReadToEnd();

                if (!string.IsNullOrEmpty(fullContent))
                {
                    var splitterIndex = fullContent.IndexOf(':');

                    if (splitterIndex > -1)
                    {
                        return fullContent.Substring(splitterIndex + 1).Trim();
                    }
                    return fullContent;
                }
            }

            return null;
        }
         
        private static PVOutputApiRateInformation GetApiRateInformationfromResponse(HttpResponseMessage response)
        {
            var result = new PVOutputApiRateInformation();

            if (response.Headers.Contains("X-Rate-Limit-Remaining"))
            {
                result.LimitRemaining = Convert.ToInt32(response.Headers.GetValues("X-Rate-Limit-Remaining").First());
            }

            if (response.Headers.Contains("X-Rate-Limit-Limit"))
            {
                result.CurrentLimit = Convert.ToInt32(response.Headers.GetValues("X-Rate-Limit-Limit").First());
            }

            if (response.Headers.Contains("X-Rate-Limit-Reset"))
            {
                result.LimitResetAt = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(response.Headers.GetValues("X-Rate-Limit-Reset").First())).DateTime;
            }

            return result;
        }

        private static Task<Stream> GetResponseContentStreamAsync(HttpResponseMessage response)
        {
            return response.Content != null ? response.Content.ReadAsStreamAsync() : Task.FromResult(default(Stream));
        }

        private static HttpRequestMessage CreateRequestMessage(IRequest request)
        {
            return new HttpRequestMessage(request.Method, CreateUrl(request));
        }

        private static string CreateUrl(IRequest request)
        {
            var requestTemplate = new UriTemplate(request.UriTemplate);

            foreach (KeyValuePair<string, object> parameter in request.GetUriPathParameters())
            {
                requestTemplate.AddParameter(parameter.Key, parameter.Value);
            }

            var apiUri = requestTemplate.Resolve();
            return $"{PVOutputClient.PVOutputBaseUri}{apiUri}";
        }

        internal Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            SetRequestHeaders(requestMessage);
            return _client.HttpClientProvider.GetHttpClient().SendAsync(requestMessage, cancellationToken);
        }

        protected void SetRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-Pvoutput-Apikey", _client.ApiKey);
            request.Headers.Add("X-Pvoutput-SystemId", _client.OwnedSystemId.ToString());
            request.Headers.Add("X-Rate-Limit", "1");
        }
    }
}
