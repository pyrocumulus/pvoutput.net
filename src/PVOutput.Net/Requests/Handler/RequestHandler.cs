using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        private ILogger Logger => _client.Logger;

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

                IObjectStringReader<TResponseContentType> reader = StringFactoryContainer.CreateObjectReader<TResponseContentType>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        private PVOutputApiError ProcessHttpErrorResults(HttpResponseMessage response, Stream responseStream)
        {
            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation(LoggingEvents.RequestStatusSuccesful, "Request successful - Status {StatusCode}", response.StatusCode);
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

            Logger.LogError(LoggingEvents.RequestStatusFailed, "Request failed - Status {StatusCode} - Content - {Message} ", error.StatusCode, error.Message);

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
                result.LimitRemaining = Convert.ToInt32(response.Headers.GetValues("X-Rate-Limit-Remaining").First(), CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (response.Headers.Contains("X-Rate-Limit-Limit"))
            {
                result.CurrentLimit = Convert.ToInt32(response.Headers.GetValues("X-Rate-Limit-Limit").First(), CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (response.Headers.Contains("X-Rate-Limit-Reset"))
            {
                result.LimitResetAt = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(response.Headers.GetValues("X-Rate-Limit-Reset").First(), CultureInfo.CreateSpecificCulture("en-US"))).DateTime;
            }

            return result;
        }

        private async Task<Stream> GetResponseContentStreamAsync(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return default;
            }

            if (Logger.IsEnabled(LogLevel.Debug))
            {
                return await LogResponseContentStreamAsync(response).ConfigureAwait(false);
            }

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private async Task<Stream> LogResponseContentStreamAsync(HttpResponseMessage response)
        {
            var cloneStream = new MemoryStream();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            await stream.CopyToAsync(cloneStream).ConfigureAwait(false);
            stream.Seek(0, SeekOrigin.Begin);
            cloneStream.Seek(0, SeekOrigin.Begin);

            using (TextReader textReader = new StreamReader(cloneStream))
            {
                string completeContent = textReader.ReadToEnd();

                if (completeContent.Length > 0)
                {
                    Logger.LogDebug(LoggingEvents.ReceivedResponseContent, "Response content" + Environment.NewLine + "{content}", completeContent);
                }
            }

            return stream;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Logging")]
        internal Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            SetRequestHeaders(requestMessage);
            Logger.LogDebug(LoggingEvents.ExecuteRequest, "Executing request - {RequestUri}", requestMessage.RequestUri);
            return _client.HttpClientProvider.GetHttpClient().SendAsync(requestMessage, cancellationToken);
        }

        protected void SetRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-Pvoutput-Apikey", _client.ApiKey);
            request.Headers.Add("X-Pvoutput-SystemId", FormatHelper.GetValueAsString<int>(_client.OwnedSystemId));
            request.Headers.Add("X-Rate-Limit", "1");
        }
    }
}
