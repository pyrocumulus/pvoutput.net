using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
    internal sealed class RequestHandler
    {
        private PVOutputClient Client { get; }

        private ILogger Logger => Client.Logger;

        private static Action<ILogger, string, Exception> LogRequestStatusSuccesful { get; set; }
        private static Action<ILogger, string, string, Exception> LogRequestStatusFailed { get; set; }
        private static Action<ILogger, int, int, DateTime, Exception> LogApiRateInformation { get; set; }
        private static Action<ILogger, string, Exception> LogReceivedResponseContent { get; set; }
        private static Action<ILogger, string, Exception> LogExecuteRequest { get; set; }
        private static Func<ILogger, Dictionary<string, object>, IDisposable> LogExecuteSingleItemRequestScope { get; set; }
        private static Func<ILogger, Dictionary<string, object>, IDisposable> LogExecuteArrayRequestScope1 { get; set; }
        private static Func<ILogger, Dictionary<string, object>, IDisposable> LogExecutePostRequestScope1 { get; set; }

        public RequestHandler(PVOutputClient client)
        {
            LogRequestStatusSuccesful = LoggerMessage.Define<string>(LogLevel.Information, LoggingEvents.Handler_RequestStatusSuccesful, "[RequestSuccessful] Status: {StatusCode}");
            LogRequestStatusFailed = LoggerMessage.Define<string, string>(LogLevel.Information, LoggingEvents.Handler_RequestStatusFailed, "[RequestFailed] Status: {StatusCode} Content: {Message}");
            LogApiRateInformation = LoggerMessage.Define<int, int, DateTime>(LogLevel.Debug, LoggingEvents.Handler_ApiInformation, "[API-rate] Remaining: {LimitRemaining} Limit: {CurrentLimit}: ResetAt: {LimitResetAt}");
            LogReceivedResponseContent = LoggerMessage.Define<string>(LogLevel.Trace, LoggingEvents.Handler_ReceivedResponseContent, "Response content:" + Environment.NewLine + "{content}");
            LogExecuteRequest = LoggerMessage.Define<string>(LogLevel.Trace, LoggingEvents.Handler_ExecuteRequest, "[ExecuteRequest] Uri: {RequestUri}");

            LogExecuteSingleItemRequestScope = LoggerMessage.DefineScope<Dictionary<string, object>>("[SingleRequest]: {SingleValues}");
            LogExecuteArrayRequestScope1 = LoggerMessage.DefineScope<Dictionary<string, object>>("[ArrayRequest]: {ArrayValues}");
            LogExecutePostRequestScope1 = LoggerMessage.DefineScope<Dictionary<string, object>>("[PostRequest]: {PostValues}");

            Client = client;
        }

        internal async Task<PVOutputResponse<TResponseContentType>> ExecuteSingleItemRequestAsync<TResponseContentType>(IRequest request, Dictionary<string, object> loggingScope, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                using (LogExecuteSingleItemRequestScope(Logger, loggingScope))
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
            }
            finally
            {
                responseMessage.Dispose();
            }
        }

        internal async Task<PVOutputArrayResponse<TResponseContentType>> ExecuteArrayRequestAsync<TResponseContentType>(IRequest request, Dictionary<string, object> loggingScope, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                using (LogExecuteArrayRequestScope1(Logger, loggingScope))
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
            }
            finally
            {
                responseMessage.Dispose();
            }
        }

        internal async Task<PVOutputBasicResponse> ExecutePostRequestAsync(IRequest request, Dictionary<string, object> loggingScope, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                using (LogExecutePostRequestScope1(Logger, loggingScope))
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
            }
            finally
            {
                responseMessage.Dispose();
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
                LogRequestStatusSuccesful(Logger, response.StatusCode.ToString(), null);
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

            LogRequestStatusFailed(Logger, error.StatusCode.ToString(), error.Message, null);

            if (Client.ThrowResponseExceptions)
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

        private PVOutputApiRateInformation GetApiRateInformationfromResponse(HttpResponseMessage response)
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

            LogApiRateInformation(Logger, result.LimitRemaining, result.CurrentLimit, result.LimitResetAt, null);
            return result;
        }

        private async Task<Stream> GetResponseContentStreamAsync(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return default;
            }

            if (Logger.IsEnabled(LogLevel.Trace))
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
                string completeContent = await textReader.ReadToEndAsync().ConfigureAwait(false);

                if (completeContent.Length > 0)
                {
                    LogReceivedResponseContent(Logger, completeContent, null);
                }
            }

            return stream;
        }

        private static HttpRequestMessage CreateRequestMessage(IRequest request)
        {
            return new HttpRequestMessage(request.Method, CreateUrl(request));
        }

        internal static string CreateUrl(IRequest request)
        {
            return CreateUrl(new UriTemplate(request.UriTemplate), request.GetUriPathParameters());
        }

        internal static string CreateUrl(UriTemplate template, IDictionary<string, object> pathParameters)
        {
            foreach (KeyValuePair<string, object> parameter in pathParameters)
            {
                template.AddParameter(parameter.Key, parameter.Value);
            }

            var apiUri = template.Resolve();
            return $"{PVOutputClient.PVOutputBaseUri}{apiUri}";
        }

        internal Task<HttpResponseMessage> ExecuteRequestAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            SetRequestHeaders(requestMessage);
            LogExecuteRequest(Logger, requestMessage.RequestUri.ToString(), null);
            return Client.HttpClientProvider.GetHttpClient().SendAsync(requestMessage, cancellationToken);
        }

        private void SetRequestHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("X-Pvoutput-Apikey", Client.ApiKey);
            request.Headers.Add("X-Pvoutput-SystemId", FormatHelper.GetValueAsString<int>(Client.OwnedSystemId));
            request.Headers.Add("X-Rate-Limit", "1");
        }
    }
}
